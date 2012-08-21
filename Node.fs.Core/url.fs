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
            href = null;
            protocol = null;
            host = null;
            auth = null;
            hostname = null;
            port = null;
            pathname = null;
            search = null;
            path = null;
            query = null;
            hash = null;
         }
        
end

module tests = 

    let run = 
        let u = new url()
        let parsedUrl = u.parse "http://user:pass@host.com:8080/p/a/t/h?query=string#hash"

        assert( parsedUrl.href = "http://user:pass@host.com:8080/p/a/t/h?query=string#hash")

