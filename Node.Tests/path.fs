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

    [<Fact>]
    let ``sep returns directory separator`` ()=
        path.sep |> should equal System.IO.Path.DirectorySeparatorChar
    
    [<Fact>]
    let ``extname 'index.html' returns .html`` ()=
        let ext = path.extname "index.html"
        ext |> should equal ".html"

    [<Fact>]
    let ``extname 'index.' returns .`` ()=
        let ext = path.extname "index."
        ext |> should equal "."

    [<Fact>]
    let ``extname 'index' returns empty string`` ()=
        let ext = path.extname "index" 
        ext |> should equal ""

    [<Fact>]
    let ``dirname '\foo' returns \`` ()=
        let dir = path.dirname @"\foo"
        dir |> should equal @"\"

    [<Fact>]
    let ``dirname '\foo\bar\baz\asdf\quux' returns \foo\bar\baz\asdf`` ()=
        let dir = path.dirname @"\foo\bar\baz\asdf\quux"
        dir |> should equal @"\foo\bar\baz\asdf"

    [<Fact>]
    let ``basename \foo\bar\baz\asdf\quux.html' returns quux.html`` ()=
        let name = path.basename @"\foo\bar\baz\asdf\quux.html"
        name |> should equal @"quux.html"

    [<Fact>]
    let ``basename \foo\bar\baz\asdf\quux.html, .html returns quux`` ()=
        let name = path.basename(@"\foo\bar\baz\asdf\quux.html", ".html")
        name |> should equal @"quux"

    [<Fact>]
    let ``basename foo.txt, .html returns foo.txt`` ()=
        let name = path.basename(@"foo.txt", ".html")
        name |> should equal @"foo.txt"
    
    [<Fact>]
    let ``normalize C:\foo\bar\\baz\asdf\quux\.. returns C:\foo\bar\baz\asdf`` ()=
        let name = path.normalize @"C:\foo\bar\\baz\asdf\quux\.."
        name |> should equal @"C:\foo\bar\baz\asdf"

