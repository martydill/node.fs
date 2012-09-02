namespace Node.fs.Core.net

type socket = class
    
    member self.addListener(eventName, callback) =
        ()

    member self.endSocket message =    // TODO end
        ()

    member self.remoteAddress = 
        ""
end

type netServer(requestHandlerFunction:(socket -> unit)) = class
     
    let mutable listener:System.Net.Sockets.TcpListener = null

     // TODO - other parameters
    member self.listen(port:int, ?host, ?backlog, ?listeningListener) =
       listener <- new System.Net.Sockets.TcpListener(port) // TODO
       listener.Start()
    
    member self.close = 
        listener.Stop()

    member self.addHandler = 
        ()

end


type net = class

    new() = {}

    member self.createServer f = 
        new netServer(f)

end


