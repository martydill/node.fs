namespace Node.fs.Core.Url


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



type url = class

    new() = {}

    member self.parse urlString = 

        let theParsedUrl = new System.Uri(urlString)

        { 
            href = theParsedUrl.OriginalString;
            protocol = theParsedUrl.Scheme + ":";
            host = theParsedUrl.Host + ":" + theParsedUrl.Port.ToString();
            auth = theParsedUrl.UserInfo;
            hostname = theParsedUrl.Host;
            port = theParsedUrl.Port.ToString();
            pathname = theParsedUrl.AbsolutePath;
            search = "?" + theParsedUrl.Query.Substring(1, theParsedUrl.Query.Length - 1);
            path = theParsedUrl.PathAndQuery;
            query = theParsedUrl.Query.Substring(1, theParsedUrl.Query.Length - 1)
            hash = theParsedUrl.Fragment;
         }
        
end

module tests = 

    let check(x, y)=
        if x = y then
            ()
        else
            printfn "**%s** = **%s**\t\tFalse" x y

    do
        
        let u = new url()
        let parsedUrl = u.parse "http://user:pass@host.com:8080/p/a/t/h?query=string#hash"

        check(parsedUrl.href, "http://user:pass@host.com:8080/p/a/t/h?query=string#hash")
        check(parsedUrl.protocol, "http:")
        check(parsedUrl.host, "host.com:8080")
        check(parsedUrl.auth, "user:pass")
        check(parsedUrl.hostname, "host.com")
        check(parsedUrl.port, "8080")
        check(parsedUrl.pathname, "/p/a/t/h")
        check(parsedUrl.search, "?query=string")
        check(parsedUrl.path, "/p/a/t/h?query=string")
        check(parsedUrl.query, "query=string")
        check(parsedUrl.hash, "#hash")
