module Test.``emitter``

open node
open Node.emitter
open Xunit
open FsUnit.Xunit

type ``Given an emitter`` ()=

    let emitter = require<emitter>
    do EmitterMethods.tickMethod <- node.nextTick

    [<Fact>]
    let ``on() adds a listener`` ()=
        let wasCalled = ref false
        let handler = fun x -> wasCalled := true
        emitter.on("foo", handler)
        emitter.emit("foo","A")
        node.tick()

        !wasCalled |> should equal true
        
    [<Fact>]
    let ``addListener() adds a listener`` ()=
        let wasCalled = ref false
        let handler = fun x -> wasCalled := true
        emitter.addListener("foo", handler)
        emitter.emit("foo","A")
        node.tick()

        !wasCalled |> should equal true

    [<Fact>]
    let ``emit() passes its argument to the listeners`` ()=
        emitter.addListener("foo", fun data ->
            data |> should equal "A"
        )
        emitter.emit("foo","A")
        node.tick()
       
    [<Fact>]
    let ``once() adds a one-time listener`` ()=
        let count = ref 0
        let listener = fun x -> count := !count + 1
        emitter.once("foo", listener)
        emitter.emit("foo","A")
        emitter.emit("foo","A")
        node.tick()

        !count |> should equal 1
        
    [<Fact>]
    let ``removeListener("foo", listener) removes the listener from the event`` ()=
        let count = ref 0
        let listener = fun x -> count := !count + 1
        emitter.addListener("foo", listener)
        emitter.emit("foo","A")
        node.tick()
        emitter.removeListener("foo", listener)
        emitter.emit("foo","B")
        node.tick()

        !count |> should equal 1

    [<Fact>]
    let ``removeAllListeners("foo") removes all of the listeners from the event`` ()=
        let count = ref 0
        emitter.addListener("foo", fun y -> count := !count + 1)
        emitter.addListener("foo", fun x -> count := !count + 1)
        emitter.removeAllListeners("foo")
        emitter.emit("foo","B")
        node.tick()

        !count |> should equal 0

    [<Fact>]
    let ``removeAllListeners() removes all of the listeners from all events`` ()=
        let count = ref 0
        emitter.addListener("foo", fun y -> count := !count + 1)
        emitter.addListener("bar", fun x -> count := !count + 1)
        emitter.removeAllListeners()
        emitter.emit("foo","B")
        emitter.emit("bar", "A")
        node.tick()

        !count |> should equal 0

    [<Fact>]
    let ``all handlers get called when emitting an event`` ()=
        let handler1WasCalled = ref false
        let handler2WasCalled = ref false
        emitter.addListener("foo", fun x -> handler1WasCalled := true)
        emitter.addListener("foo", fun x -> handler2WasCalled := true)
        emitter.emit("foo", "A")
        node.tick()

        !handler1WasCalled |> should equal true 
        !handler2WasCalled |> should equal true

    [<Fact>]
    let ``listeners() returns an array of all the listeners`` ()=
        emitter.addListener("foo", fun x -> ())
        emitter.addListener("foo", fun x -> ())
        emitter.listeners("foo").Length |> should equal 2


               
    interface System.IDisposable with
        member x.Dispose() = 
            node.stop
            node.eventsForNextTick.Clear()
            emitter.removeAllListeners()
