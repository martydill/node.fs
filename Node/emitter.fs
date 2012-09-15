﻿namespace Node.emitter

open System

// http://nodejs.org/api/events.html#events_class_events_eventemitter
type Listener = {func:Object; isOneTime:bool}

type emitter() = class
    
    let _handlerMap = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Listener>>()

    member self.addListenerInternal(event, listener, oneTime) =
        let handlerList = 
            match _handlerMap.TryGetValue(event) with
            | true, handlerList -> handlerList
            | false, _ -> new System.Collections.Generic.List<Listener>()
        
        let myListener = {func = listener; isOneTime = oneTime}

        handlerList.Add(myListener)
        _handlerMap.[event] <- handlerList

    member self.addListener(event, listener) = 
        self.addListenerInternal(event, listener, false)

    member self.on(event, listener) = 
        self.addListenerInternal(event, listener, false)

    member self.once(event, listener) = 
        self.addListenerInternal(event, listener, true)
    
    member self.removeListener(event, listener) =

        // TODO - find a better way to do this
        // Check to see if the function matches by checking it's type's FullName.
        // This *seems* to work, but it sucks.
        // Can't just compare funcs directly. http://stackoverflow.com/a/8226506/184630
        match _handlerMap.TryGetValue(event) with
            | true, handlerList -> 
                handlerList.RemoveAll(fun h -> h.func.GetType().FullName = listener.GetType().FullName) |> ignore
            | false, _ -> ()

    member self.removeAllListeners(?event) =         
        match event with
            | Some e -> 
                match _handlerMap.TryGetValue(e) with
                    | true, handlerList -> 
                        handlerList.Clear() |> ignore
                    | false, _ -> ()
            | None -> _handlerMap.Clear()

    member self.setMaxListeners(n) =
        ()
    
    member self.listeners(event) = 
        match _handlerMap.TryGetValue(event) with
            | true, handlers -> handlers.ToArray() // TODO - should this return Listeners, or just the actual handlers?
            | false, _ -> Array.zeroCreate<Listener> 0

    member private self.fire(handler:System.Object, args) =
        let f = handler :?> Microsoft.FSharp.Core.FSharpFunc<System.Object, Microsoft.FSharp.Core.Unit>
        f.Invoke args
        
    member private self.fireAll(handlerList:System.Collections.Generic.List<Listener>, args) =
        for handler in handlerList do
            self.fire(handler.func, args)

        handlerList.RemoveAll(fun listener -> listener.isOneTime) |> ignore

    member self.emit(event, args) =
        match _handlerMap.TryGetValue(event) with
        | true, handlers -> self.fireAll(handlers, args) |> ignore
        | false, _ -> ()
        
end
