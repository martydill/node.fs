module Test.``fs - readStream``

open node
open Node.fs
open Xunit
open FsUnit.Xunit
open System.Linq
open System.Threading

type ``Given a file that exists`` ()=

    let fs = require<fs>
    let path = System.IO.Path.GetTempFileName()
    let bytes = [|0x0uy;0x1uy;0x2uy;0x3uy|]

    let ignored = System.IO.File.WriteAllBytes(path, bytes)
    let stream = fs.createReadStream(path)
    
    [<Fact>]
    let ``createReadStream creates a ReadableStream`` ()=
        stream |> should be ofExactType<Node.stream.FileStream>

    [<Fact>]
    let ``createReadStream creates a stream with readable to true`` ()=
        stream.readable |> should equal true

    [<Fact>]
    let ``createReadStream fires the data event with the file's data`` ()=

        Node.emitter.EmitterMethods.tickMethod <- fun x -> nextTick(x)
        node.start( fun () ->
            let eventFired = ref false
            stream.addListener("data", fun data ->
                let fileBytes = data :?> byte[]
                fileBytes.SequenceEqual(bytes) |> should equal true
                node.stop()
            )
        )

    [<Fact>]
    let ``destroy sets readable to false`` ()=
        stream.destroy()
        stream.readable |> should equal false

    interface System.IDisposable with
        member x.Dispose() = 
            stream.destroy()
            System.IO.File.Delete path
