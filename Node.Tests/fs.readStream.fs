module Test.``fs - readStream``

open Node.fs
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given a file that exists`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    let bytes = [|0x0uy;0x1uy;0x2uy;0x3uy|]

    let ignored = System.IO.File.WriteAllBytes(path, bytes)
    let readStream = fs.createReadStream(path)
    
    [<Fact>]
    let ``createReadStream creates a readable stream`` ()=
        let data = Array.zeroCreate<byte> 4
        fs.readSync(file, data, 0, data.Length, 0) |> ignore
        data.SequenceEqual(bytes) |> should equal true

    interface System.IDisposable with
        member x.Dispose() = 
            file.Dispose()
            System.IO.File.Delete path
