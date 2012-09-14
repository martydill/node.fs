namespace Node.emitter

// http://nodejs.org/api/events.html#events_class_events_eventemitter

type emitter() = class
    
    let _handlerMap = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Object>>()

    member self.addListener(event, listener) = 

        let handlerList = 
            match _handlerMap.TryGetValue(event) with
            | true, handlerList -> handlerList
            | false, _ -> new System.Collections.Generic.List<System.Object>()
        
        handlerList.Add(listener)
        _handlerMap.[event] <- handlerList

    member self.on(event, listener) = 
        self.addListener(event, listener)

    member self.once(event, listener) = 
        ()
    
    member self.removeListener(event, listener) =
        ()

    member self.removeAllListeners(?event) = 
        ()

    member self.setMaxListeners(n) =
        ()
    
    member self.listeners(event) = 
        Array.zeroCreate<System.Object> 0

    member private self.fire(handler:System.Object, args) =
        let f = handler :?> Microsoft.FSharp.Core.FSharpFunc<System.Object, Microsoft.FSharp.Core.Unit>
        f.Invoke args
        
    member private self.fireAll(handlerList:System.Collections.Generic.List<System.Object>, args) =
        for handler in handlerList do
            self.fire(handler, args)

    member self.emit(event, args) =

        match _handlerMap.TryGetValue(event) with
        | true, handler -> self.fireAll(handler, args) |> ignore
        | false, _ -> ()


        
end
