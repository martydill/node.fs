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
            host = theParsedUrl.Host + ":" + theParsedUrl.Port.ToString();
            auth = theParsedUrl.UserInfo;
            hostname = theParsedUrl.Host;
            port = theParsedUrl.Port.ToString();
            pathname = theParsedUrl.AbsolutePath;
            search = "?" + query;
            path = theParsedUrl.PathAndQuery;
            query = query;
            hash = theParsedUrl.Fragment;
         }
        
    member self.format urlobj =
        raise (System.NotImplementedException())

    member self.resolve(fromUrl, toUrl) =
        raise (System.NotImplementedException())
end
