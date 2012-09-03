namespace Node.fs.Core.net
open helpers

type socket(theSocket:System.Net.Sockets.Socket) = class
    
    member self.write(data, ?encoding0, ?callback) = 
       
        let encoding = defaultArg encoding0 Utf8

        // TODO async
        let bytes = helpers.getBytes(data, encoding)
        theSocket.Send(bytes) |> ignore

    member self.endSocket(data, ?encoding:string) =    // TODO end
        self.write(data)
        let z = encoding
        theSocket.Close |> ignore
        
    member self.remoteAddress = 
        theSocket.RemoteEndPoint.ToString()

end

type netServer(requestHandlerFunction:(socket -> unit)) = class
     
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
            requestHandlerFunction socket
        }

end


type net = class

    new() = {}

    member self.createServer f = 
        new netServer(f)

end


