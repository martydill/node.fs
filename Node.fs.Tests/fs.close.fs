module Test.``fs - close``

open System.IO
open Node.fs.Core.fs
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given an open file`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    let file = fs.openSync(path, "r")

    [<Fact>]
    let ``closeSync should close it`` ()=
        fs.closeSync(file)
        file.CanRead |> should equal false
    
    [<Fact>]
    let ``close should close it asynchronously`` ()=
        fs.close(file, fun () ->
            file.CanRead |> should equal false
        )
        
    interface System.IDisposable with
        member x.Dispose() = 
            if file <> null then file.Close()
            if File.Exists(path) then System.IO.File.Delete path

