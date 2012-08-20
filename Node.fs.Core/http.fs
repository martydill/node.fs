namespace Node.fs.Core.Http


type httpServerRequest = class
    
    val req : System.Net.HttpListenerRequest

    new(httpListenerRequest) = { req = httpListenerRequest }

end


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


type httpServer(func) = class

    member self.requestHandler(request, response) = 
        func(request, response)

    member self.listen (port:int) = 
        let listener = new System.Net.HttpListener()
        let prefix = "http://localhost:" + port.ToString() + "/"
        listener.Prefixes.Add(prefix)
        printfn "Listening at %s" prefix
        listener.Start()
        let context = listener.GetContext()
        printfn "Got context"
        self.requestHandler(new httpServerRequest(context.Request), new httpServerResponse(context.Response))
        
//        let bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("Hello World!");
//        
//        context.Response.OutputStream.Write(bytes, 0, bytes.Length);
//        context.Response.Close();
        //listener.Stop();
        ()

end

type http = class

    new() = {}

    member self.createServer f = 
        new httpServer(f)

end
