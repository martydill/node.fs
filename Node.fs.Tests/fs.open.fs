module Test.``fs - open``

open Node.fs.Core.fs
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given an existing file`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    let mutable file:System.IO.FileStream = null

    [<Fact>]
    let ``openSync r should open it for reading`` ()=
        file <- fs.openSync(path,"r") 
        file.CanRead |> should equal true
        file.CanWrite |> should equal false
    
    [<Fact>]
    let ``openSync w should open it for reading`` ()=
        file <- fs.openSync(path,"w") 
        file.CanRead |> should equal false
        file.CanWrite |> should equal true

    [<Fact>]
    let ``openSync r+ should open it for reading and writing`` ()=
        file <- fs.openSync(path,"r+") 
        file.CanRead |> should equal true
        file.CanWrite |> should equal true


    interface System.IDisposable with
        member x.Dispose() = 
            if file <> null then file.Close()
            System.IO.File.Delete path
