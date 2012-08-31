namespace UrlTests
open Node.fs.Core.Url
open Xunit
open FsUnit.Xunit

type ``Given the url`` ()=
    
    let u = new url()
    let parsedUrl = u.parse  "http://user:pass@host.com:8080/p/a/t/h?query=string#hash"

    [<Fact>]
    let ``the protocol should be http:`` ()=
        parsedUrl.protocol |> should equal "http:"

    [<Fact>]
    let ``the host should be host.com:8080`` ()=
        parsedUrl.host |> should equal "host.com:8080"

    [<Fact>]
    let ``the auth should be user:pass`` ()=
        parsedUrl.auth |> should equal "user:pass"
   
    [<Fact>]
    let ``the hostname should be host.com`` ()=
        parsedUrl.hostname |> should equal "host.com"
    
    [<Fact>]
    let ``the port should be 8080`` ()=
        parsedUrl.port |> should equal "8080"
   
    [<Fact>]
    let ``the pathname should be /p/a/t/h`` ()=
        parsedUrl.pathname |> should equal "/p/a/t/h"
  
    [<Fact>]
    let ``the search should be ?query=string`` ()=
        parsedUrl.search |> should equal "?query=string"
  
    [<Fact>]
    let ``the path should be /p/a/t/h?query=string`` ()=
        parsedUrl.path |> should equal "/p/a/t/h?query=string"
    
    [<Fact>]
    let ``the query should be query=string`` ()=
        parsedUrl.query |> should equal "query=string"

    [<Fact>]
    let ``the hash should be #hash`` ()=
        parsedUrl.hash |> should equal "#hash"
