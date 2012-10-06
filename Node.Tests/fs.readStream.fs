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

        let eventFired = ref false
        node.start( fun () ->
            stream.addListener("data", fun data ->
                eventFired := true
                let fileBytes = data :?> byte[]
                fileBytes.SequenceEqual(bytes) |> should equal true
                node.stop()
            )
        )

        !eventFired |> should equal true

    [<Fact>]
    let ``createReadStream fires the end event when there is no more data`` ()=

        let eventFired = ref false
        node.start( fun () ->
            stream.addListener("end", fun data ->
               eventFired := true
               node.stop()
            )
        )

        !eventFired |> should equal true

    [<Fact>]
    let ``destroy sets readable to false`` ()=
        stream.destroy()
        stream.readable |> should equal false

    interface System.IDisposable with
        member x.Dispose() = 
            stream.destroy()
            System.IO.File.Delete path
