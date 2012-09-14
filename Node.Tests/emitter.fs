module Test.``emitter``

open Node.emitter
open Xunit
open FsUnit.Xunit

type ``Given an emitter`` ()=

    let emitter = new emitter()
    
    [<Fact>]
    let ``on() adds a handler`` ()=
        let wasCalled = ref false
        let handler = fun x -> wasCalled := true
        emitter.on("foo", handler)
        emitter.emit("foo","A")
        
        !wasCalled |> should equal true
        
    [<Fact>]
    let ``addListener() adds a handler`` ()=
        let wasCalled = ref false
        let handler = fun x -> wasCalled := true
        emitter.addListener("foo", handler)
        emitter.emit("foo","A")
        
        !wasCalled |> should equal true

    [<Fact>]
    let ``emit() passes its argument to the handler`` ()=
        emitter.addListener("foo", fun data ->
            data |> should equal "A"
        )
        emitter.emit("foo","A")


    [<Fact>]
    let ``all handlers get called when emitting an event`` ()=
        let handler1WasCalled = ref false
        let handler2WasCalled = ref false
        emitter.addListener("foo", fun x -> handler1WasCalled := true)
        emitter.addListener("foo", fun x -> handler2WasCalled := true)
        emitter.emit("foo", "A")
       
        !handler1WasCalled |> should equal true 
        !handler2WasCalled |> should equal true
        
    