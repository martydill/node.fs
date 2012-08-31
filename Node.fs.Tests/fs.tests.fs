namespace FsTests
open Node.fs.Core.fs
open Xunit
open FsUnit.Xunit

type ``Given a file that exists`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    
    [<Fact>]
    let ``existsSync should return true`` ()=
        let exists = fs.existsSync path
        exists |> should equal true

    [<Fact>]
    let ``exists should pass true to the callback`` ()=
        fs.exists(path, fun exists ->
             exists |> should equal true
        )

    interface System.IDisposable with
        member x.Dispose() = 
            System.IO.File.Delete path

   

   
type ``Given a file that does not exist`` ()=

    let fs = new fs()
    let path = "DoesNotExists.txt"
    
    [<Fact>]
    let ``existsSync should return false`` ()=
        let exists = fs.existsSync path
        exists |> should equal false

    [<Fact>]
    let ``exists should pass false to the callback`` ()=
        fs.exists(path, fun exists ->
             exists |> should equal false
        )

    interface System.IDisposable with
        member x.Dispose() = 
            System.IO.File.Delete path

   