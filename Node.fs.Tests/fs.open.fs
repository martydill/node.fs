module Test.``fs - open``

open System.IO
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


type ``Given a nonexistent file`` ()=

    let fs = new fs()
    let path = Path.Combine(Path.GetTempPath(), "nonexistent.file")
    let mutable file:System.IO.FileStream = null
         
    [<Fact>]
    let ``openSync r should throw an exception`` ()=
        (fun() -> fs.openSync(path,"r") |> ignore)
            |> should throw typeof<FileNotFoundException>
          
    [<Fact>]
    let ``openSync w should create the file`` ()=
       file <- fs.openSync(path,"w")
       File.Exists(path) |> should equal true

    [<Fact>]
    let ``openSync r+ should throw an exception`` ()=
          (fun() -> fs.openSync(path,"r+") |> ignore)
            |> should throw typeof<FileNotFoundException>

    interface System.IDisposable with
        member x.Dispose() = 
            if file <> null then file.Close()
            if File.Exists(path) then File.Delete path
