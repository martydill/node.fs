namespace Node.fs.Core.Http


type httpServerRequest(httpListenerRequest: System.Net.HttpListenerRequest) =
    
    let req = httpListenerRequest

    do
        printfn "a"
        let data = Array.zeroCreate 1024
        req.InputStream.BeginRead(data, 0, 1024, fun callback ->
            printfn "Got data"
        , null) |> ignore


    let mutable dataHandler = ()
    let mutable endHandler = ()
    let mutable closeHandler = ()

    member self.url = 
        req.Url.OriginalString

    member self.addListener(eventName, func) = 
        match eventName with
        | "data" -> dataHandler <- func
        | "end" -> endHandler <- func
        | "close" -> closeHandler <- func
        | _ -> raise (System.ArgumentException("Unknown event name " + eventName))
        
        ()

    

type httpServerResponse = class
    
    val resp : System.Net.HttpListenerResponse

    new(response) = { resp = response }

    // TODO end
    member self.endResponse = 
        self.resp.Close()

    member self.write (data:string) = 
        printfn "Writing %s" data
        let bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(data)
        self.resp.OutputStream.Write(bytes, 0, bytes.Length)

    member self.writeHead (statusCode:int, headers:System.Collections.Generic.IDictionary<string, string>) = 
        
        for key in headers.Keys do
            match headers.[key] with
            | "Content-Type" -> self.resp.ContentType <- headers.[key]
            | _ -> self.resp.AddHeader(key, headers.[key])
            
        self.resp.StatusCode <- statusCode
        ()

end


type httpServer(requestHandlerFunction) = class

    member self.listen (port:int) = 
        let listener = new System.Net.HttpListener()
        let prefix = "http://localhost:" + port.ToString() + "/"
        listener.Prefixes.Add(prefix)
        printfn "Listening at %s" prefix
        listener.Start()

        Async.Start(self.listenImpl(listener))


    member self.listenImpl(listener:System.Net.HttpListener) = async {

        while listener.IsListening do
            let! context = Async.FromBeginEnd(listener.BeginGetContext, listener.EndGetContext)
            requestHandlerFunction(new httpServerRequest(context.Request), new httpServerResponse(context.Response))
        }
        
end

type http = class

    new() = {}

    member self.createServer f = 
        new httpServer(f)

end


