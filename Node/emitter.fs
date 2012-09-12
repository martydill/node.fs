namespace Node.emitter

// http://nodejs.org/api/events.html#events_class_events_eventemitter

type emitter = class

    new() = {}
        
    member self.addListener(event, listener) = 
        ()

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
        ()
end
