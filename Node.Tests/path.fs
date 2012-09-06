module Test.``path - ``

open Node.path
open Xunit
open FsUnit.Xunit

type ``Given an instance of path`` ()=

    let path = new path()
 
    [<Fact>]
    let ``join(a, b) returns a\b`` ()=
        let result = path.join("a", "b")
        result |> should equal "a\\b"

  