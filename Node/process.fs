module node_process

type proc = class // NAMING: Should be process
    new() = {}

    member self.cwd = 
        System.Environment.CurrentDirectory

end

