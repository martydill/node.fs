module node_process

// TODO - should this be part of the node class?
type proc(nextTickMethod:(unit->unit)->unit) = class // NAMING: Should be process
    
    member self.cwd = 
        System.Environment.CurrentDirectory

    member self.nextTick f = 
        nextTickMethod(f)

end

