module Test.``path - ``

open node
open Node.path
open Xunit
open FsUnit.Xunit

type ``Given an instance of path`` ()=

    let path = require<path>
 
    [<Fact>]
    let ``join(a, b) returns a\b`` ()=
        let result = path.join("a", "b")
        result |> should equal "a\\b"

    [<Fact>]
    let ``join(a, null, b) returns a\b`` ()=
        let result = path.join("a", null, "b")
        result |> should equal "a\\b"

    // Node does this, .NET's Path.Combine() doesn't...
    [<Fact>]
    let ``join(a, /b) returns a\b`` ()=     
        let result = path.join("a", null, "/b")
        result |> should equal "a\\b"
  
  