module Test.url

open node
open Node.Url
open Xunit
open FsUnit.Xunit

type ``Given the http url`` ()=
    
    let u = require<url>

    let originalUrl = "http://user:pass@host.com:8080/p/a/t/h?query=string#hash"
    let parsedUrl = u.parse originalUrl
    let formattedUrl = u.format parsedUrl

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

    [<Fact>]
    let ``format returns the original url`` ()=
        formattedUrl |> should equal originalUrl



type ``Given the mailto url`` ()=

    let u = require<url>
    let originalUrl = "mailto:foo@bar.com"
    let parsedUrl = u.parse originalUrl
    let formattedUrl = u.format parsedUrl

    [<Fact>]
    let ``the protocol should be mailto:`` ()=
        parsedUrl.protocol |> should equal "mailto:"

    [<Fact>]
    let ``the auth should be mailto:`` ()=
        parsedUrl.auth |> should equal "foo"

    [<Fact>]
    let ``the host should be mailto:`` ()=
        parsedUrl.host |> should equal "bar.com"

    [<Fact>]
    let ``the hostname should be mailto:`` ()=
        parsedUrl.hostname|> should equal "bar.com"

    [<Fact>]
    let ``the href should be mailto:`` ()=
        parsedUrl.href |> should equal "mailto:foo@bar.com"
