module Test.``fs - read``

open Node.fs
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given a file that exists`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    let bytes = [|0x0uy;0x1uy;0x2uy;0x3uy|]

    let ignored = System.IO.File.WriteAllBytes(path, bytes)
    let file = fs.openSync(path, "r")
    
    [<Fact>]
    let ``readSync should fill the buffer with its data`` ()=
        let data = Array.zeroCreate<byte> 4
        fs.readSync(file, data, 0, data.Length, 0) |> ignore
        data.SequenceEqual(bytes) |> should equal true

    [<Fact>]
    let ``readSync should return the number of bytes read`` ()=
        let data = Array.zeroCreate<byte> 4
        let numberOfBytes = fs.readSync(file, data, 0, data.Length, 0)
        numberOfBytes |> should equal 4

    [<Fact>]
    let ``read should fill the buffer with its data`` ()=
        let data = Array.zeroCreate<byte> 4
        fs.read(file, data, 0, data.Length, 0, fun(_,_,_) ->
            data.SequenceEqual(bytes) |> should equal true
        )

    [<Fact>]
    let ``read should pass (err, bytesRead, buffer) to the callback`` ()=
        let data = Array.zeroCreate<byte> 4
        fs.read(file, data, 0, data.Length, 0, fun(err, bytesRead, buffer) ->
           buffer |> should equal data
           err |> should equal null
           bytesRead |> should equal 4
        )

    interface System.IDisposable with
        member x.Dispose() = 
            file.Dispose()
            System.IO.File.Delete path
