namespace Node.fs.Core.Http


type httpServerRequest(httpListenerRequest: System.Net.HttpListenerRequest) =
    
    let req = httpListenerRequest

    let mutable dataHandler = fun (data:string) -> ()
    let mutable endHandler = fun (data:string) -> ()
    let mutable closeHandler = fun (data:string) -> ()

    
    member self.asyncRead = async {

        let data = Array.zeroCreate 1024
        
        let count = ref 1
        while !count > 0 do 
            let! bytesRead = req.InputStream.AsyncRead(data, 0, 1024)
            let str = System.Text.Encoding.UTF8.GetString(data, 0, bytesRead)
            count := bytesRead
            dataHandler str
            ()
        
        endHandler ""
    }   

    member self.url = 
        req.Url.OriginalString

    member self.addListener(eventName, func) = 
        match eventName with
        | "data" ->
             dataHandler <- func
             Async.Start(self.asyncRead)
        | "end" -> endHandler <- func
        | "close" -> closeHandler <- func
        | _ -> raise (System.ArgumentException("Unknown event name " + eventName))


    

type httpServerResponse = class
    
    val resp : System.Net.HttpListenerResponse

    new(response) = { resp = response }

    // TODO end
    member self.endResponse = 
        self.resp.Close()

    member self.write (data:string) = 
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

    let mutable connList = []

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
            let req = new httpServerRequest(context.Request)
            let resp = new httpServerResponse(context.Response)
            connList <- connList @ [req]
            requestHandlerFunction(req, resp)
        }
        
end

type http = class

    new() = {}

    member self.createServer f = 
        new httpServer(f)

end


