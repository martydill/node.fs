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

        // TODO - find a better way to do this
        // Check to see if the function matches by checking it's type's FullName.
        // This *seems* to work, but it sucks.
        // Can't just compare funcs directly. http://stackoverflow.com/a/8226506/184630
        match _handlerMap.TryGetValue(event) with
            | true, handlerList -> 
                handlerList.RemoveAll(fun h -> h.GetType().FullName = listener.GetType().FullName) |> ignore
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
