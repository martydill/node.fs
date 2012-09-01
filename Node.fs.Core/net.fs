namespace Node.fs.Core.net

type netServer(requestHandlerFunction:System.Object) = class
     
    let mutable listener:System.Net.Sockets.TcpListener = null

     // TODO - other parameters
    member self.listen(port:int, ?host, ?backlog, ?listeningListener) =
       listener <- new System.Net.Sockets.TcpListener(port)
       listener.Start()
    
    member self.close = 
        listener.Stop()

end


type net = class

    new() = {}

    member self.createServer f = 
        new netServer(f)

end


