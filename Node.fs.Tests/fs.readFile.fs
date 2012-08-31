module Test.``fs - readFile``

open Node.fs.Core.fs
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given a file that exists`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    let contents = "This is a test"
    let bytes = System.Text.Encoding.UTF8.GetBytes(contents)
    let ignored = System.IO.File.WriteAllBytes(path, bytes)

    
    [<Fact>]
    let ``readFileSync with no encoding should return its data`` ()=
        let data = fs.readFileSync path
        data.SequenceEqual(bytes) |> should equal true

    [<Fact>]
    let ``readFile with no encoding should pass its data to the callback`` ()=
        fs.readFile(path, fun data ->
            data.SequenceEqual(bytes) |> should equal true
        )
       

    interface System.IDisposable with
        member x.Dispose() = 
            System.IO.File.Delete path
