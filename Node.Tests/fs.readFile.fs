module Test.``fs - readFile``

open Node.fs
open Xunit
open FsUnit.Xunit
open System.Linq
open node

type ``Given a file that exists`` ()=

    let fs = new fs()
    let path = System.IO.Path.GetTempFileName()
    let contents = "This is a test"
    let bytes = System.Text.Encoding.UTF8.GetBytes(contents)
    let ignored = System.IO.File.WriteAllBytes(path, bytes)
    let waiter = new System.Threading.AutoResetEvent(false)
    
    [<Fact>]
    let ``readFileSync with no encoding should return its data`` ()=
        let data = fs.readFileSync path
        data.SequenceEqual(bytes) |> should equal true

    [<Fact>]
    let ``readFileSync with utf8 encoding should return its data as a string`` ()=
        let data = fs.readFileSync(path, Utf8)
        data |> should equal contents
    
    [<Fact>]
    let ``readFile with no encoding should pass its data to the callback`` ()=
        let isSequenceEqual = ref false
        fs.readFile(path, fun(data, error) ->
            isSequenceEqual := data.SequenceEqual(bytes) 
            waiter.Set() |> ignore
        ) |> ignore

        waiter.WaitOne() |> ignore
        !isSequenceEqual |> should equal true

    [<Fact>]
    let ``readFile should pass a null error to the callback`` ()=
        let isErrorNull = ref false
        fs.readFile(path, fun(data, error) ->
            isErrorNull := error = null
            waiter.Set() |> ignore
        ) |> ignore

        waiter.WaitOne() |> ignore
        !isErrorNull |> should equal true

    [<Fact>]
    let ``readFile with utf8 encoding should pass its string to the callback`` ()=
        let doesDataEqualContents = ref false
        fs.readFile(path, Utf8, fun(data, error) ->
            doesDataEqualContents := data = contents
            waiter.Set() |> ignore
        ) |> ignore

        waiter.WaitOne() |> ignore
        !doesDataEqualContents |> should equal true


    interface System.IDisposable with
        member x.Dispose() = 
            System.IO.File.Delete path

            
type ``Given a file that doesn't exist`` ()=

    let fs = new fs()
    let path = "nonexistent.file"
    let waiter = new System.Threading.AutoResetEvent(false)

    [<Fact>]
    let ``readFile should pass null data to the callback`` ()=
        let isDataNull = ref false
        fs.readFile(path, fun(data, error) ->
            isDataNull := data = null
            waiter.Set() |> ignore
        ) |> ignore

        waiter.WaitOne() |> ignore
        !isDataNull |> should equal true
    
    [<Fact>]
    let ``readFile should pass a FileNotFoundException as the error to the callback`` ()=
        let isErrorAFileNotFoundException = ref false
        fs.readFile(path, fun(data, error) ->
            isErrorAFileNotFoundException := error :? System.IO.FileNotFoundException
            waiter.Set() |> ignore
        ) |> ignore

        waiter.WaitOne() |> ignore
        !isErrorAFileNotFoundException |> should equal true
        
