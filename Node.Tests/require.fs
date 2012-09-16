module Test.Require

open node
open Node.Http
open Xunit
open FsUnit.Xunit

type ``Given a call to require<T>`` ()=
    
    let http = node.require<http>

    [<Fact>]
    let ``an instance of T is returned`` ()=
        http |> should be ofExactType<http>

    [<Fact>]
    let ``successive calls return the same instance`` ()=
        let http2 = node.require<http>
        http2 |> should equal http


