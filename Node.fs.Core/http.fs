namespace Node.fs.Core.Http


type httpServer = class

    new(func) = {}

    member self.listen (port:int) = 
        let listener = new System.Net.HttpListener()
        let prefix = "http://localhost:" + port.ToString() + "/"
        listener.Prefixes.Add(prefix)
        listener.Start()
        let context = listener.GetContext()
        ()

end

type http = class

    new() = {}

    member self.createServer f (a:int) (b:int) = 
        new httpServer()

end
