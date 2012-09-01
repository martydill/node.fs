namespace Node.fs.Core.net

type netServer(requestHandlerFunction:System.Object) = class
     
     member self.listen(port:int, ?host, ?backlog, ?listeningListener) =  // TODO - other parameters
        let listener = new System.Net.Sockets.TcpListener(port)
        listener.Start()


end

type net = class

    new() = {}

    member self.createServer f = 
        new netServer(f)

end


