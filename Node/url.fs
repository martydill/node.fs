namespace Node.Url


type parsedUrl = {

       href: string
       protocol: string
       host: string
       auth: string
       hostname: string
       port: string
       pathname: string
       search: string
       path: string
       query: string
       hash: string
   }


// http://nodejs.org/api/all.html#all_url
type url = class

    new() = {}

    member self.parse(urlString, ?parseQueryString, ?slashesDenoteHost)  = 
        
        let theParsedUrl = new System.Uri(urlString)
        
        let query = if theParsedUrl.Query.Length > 0 then theParsedUrl.Query.Substring(1, theParsedUrl.Query.Length - 1) else ""
        { 
            href = theParsedUrl.OriginalString;
            protocol = theParsedUrl.Scheme + ":";
            host = theParsedUrl.Host + self.getPortFor(theParsedUrl)
            auth = theParsedUrl.UserInfo;
            hostname = theParsedUrl.Host;
            port = theParsedUrl.Port.ToString();
            pathname = theParsedUrl.AbsolutePath;
            search = "?" + query;
            path = theParsedUrl.PathAndQuery;
            query = query;
            hash = theParsedUrl.Fragment;
         }
    
    member private self.getPortFor(urlobj) =
        match urlobj.Scheme with
        | "mailto" -> "" // TODO - is this the only one that shouldn't get a port?
        | _ -> ":" + urlobj.Port.ToString()

    member self.format(urlobj:parsedUrl) =
        let uri = new System.Uri(self.protocolFor(urlobj.protocol) + self.authFor(urlobj.auth) + self.hostFor(urlobj) + urlobj.pathname + self.queryFor(urlobj) + self.hashFor(urlobj))
        uri.OriginalString

    member self.resolve(fromUrl, toUrl) =
        raise (System.NotImplementedException())

    member private self.protocolFor(protocol:string) = 
        let strippedProtocol = protocol.Replace(":", "")
        match strippedProtocol with 
        | "ftp" | "http" | "https" | "gopher" | "file" -> strippedProtocol + "://"
        | _ -> strippedProtocol + ":"

    member private self.authFor(auth:string) = 
        match auth with
        | null | "" -> ""
        | _ -> auth + "@"

    member private self.hostFor(urlobj) =
        match urlobj.host with
        | null | ""   -> urlobj.hostname + ":" + urlobj.port
        | _ -> urlobj.host

    member private self.queryFor(urlobj) =
        match urlobj.search with
        | null | "" -> urlobj.query
        | _ -> urlobj.search

     member private self.hashFor(urlobj) =
        urlobj.hash

end
