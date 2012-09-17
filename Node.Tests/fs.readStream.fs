module Test.``fs - readStream``

open node
open Node.fs
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given a file that exists`` ()=

    let fs = require<fs>
    let path = System.IO.Path.GetTempFileName()
    let bytes = [|0x0uy;0x1uy;0x2uy;0x3uy|]

    let ignored = System.IO.File.WriteAllBytes(path, bytes)
    let readStream = fs.createReadStream(path)
    
    [<Fact>]
    let ``createReadStream creates a ReadableStream`` ()=
        readStream |> should be ofExactType<Node.stream.ReadableStream>

    [<Fact>]
    let ``createReadStream creates a stream with readable to true`` ()=
        readStream.readable |> should equal true

    [<Fact>]
    let ``destroy sets readable to false`` ()=
        readStream.destroy()
        readStream.readable |> should equal false

    interface System.IDisposable with
        member x.Dispose() = 
            readStream.destroy()
            //file.Dispose()
            System.IO.File.Delete path
