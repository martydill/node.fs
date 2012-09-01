module Test.``fs - read``

open Node.fs.Core.fs
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given a file that exists`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    let bytes = [|0x0uy;0x1uy;0x2uy;0x3uy|]

    let ignored = System.IO.File.WriteAllBytes(path, bytes)

//    
//    [<Fact>]
//    let ``readSync should return its data`` ()=
//        let data = fs.readSync path
//        data.SequenceEqual(bytes) |> should equal true

   


    interface System.IDisposable with
        member x.Dispose() = 
            System.IO.File.Delete path
