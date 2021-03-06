﻿namespace Node.net
open node
open Node.emitter

type socket(theSocket:System.Net.Sockets.Socket) = class
    inherit emitter()
    
    [<Literal>]
    let EndEvent = "end"

    [<Literal>]
    let DataEvent = "data"

    [<Literal>]
    let CloseEvent = "close"
        
    member self.write(data, ?encoding0, ?callback) = 
        let encoding = defaultArg encoding0 Utf8

        // TODO async
        let bytes = node.getBytes(data, encoding)
        theSocket.Send(bytes) |> ignore

    member self.endSocket(data, ?encoding) =    // TODO end
        self.write(data, ?encoding0 = encoding) // http://stackoverflow.com/a/7095907/184630
        theSocket.Close |> ignore
        
    member private self.remoteEndPoint = 
        theSocket.RemoteEndPoint :?> System.Net.IPEndPoint

    member self.remoteAddress = 
        self.remoteEndPoint.Address.ToString()
  
    member self.remotePort = 
        self.remoteEndPoint.Port.ToString()

    member private self.asyncRead = async {

        let data = Array.zeroCreate 1024
        let count = ref 1

        let mutable args = new System.Net.Sockets.SocketAsyncEventArgs()
        args.SetBuffer(data, 0, 1024)
        args.Completed.Add(fun completedArgs ->
            count := completedArgs.BytesTransferred
            let str = System.Text.Encoding.UTF8.GetString(data, 0, completedArgs.BytesTransferred)
            if !count > 0 then
                self.emit(DataEvent, str)
                Async.Start(self.asyncRead)
            else
                self.emit(EndEvent, "")
        )

        theSocket.ReceiveAsync(args) |> ignore
    }   

    member self.addListener(eventName, func) = 
        base.addListener(eventName, func)

        match eventName with
        | "data" -> Async.Start(self.asyncRead)
        | _ -> ()
  
end

type netServer(requestHandlerFunction:(socket -> unit)) = class
     
    let mutable connList = []
    let mutable listener:System.Net.Sockets.TcpListener = null

     // TODO - other parameters
    member self.listen(port:int, ?host, ?backlog, ?listeningListener) =
       listener <- new System.Net.Sockets.TcpListener(port) // TODO
       listener.Start()
       Async.Start(self.listenImpl(listener))

    member self.close = 
        listener.Stop()

    member self.addHandler = 
        ()

    member self.listenImpl(listener) = async {

        while true do
            let! context = Async.FromBeginEnd(listener.BeginAcceptTcpClient, listener.EndAcceptTcpClient)
            let socket = new socket(context.Client)
            connList <- connList @ [socket]
            requestHandlerFunction socket
        }

end


type net = class

    new() = {}

    member self.createServer f = 
        new netServer(f)

end


