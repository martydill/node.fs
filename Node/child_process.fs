namespace Node.child_process

open System.Diagnostics


type ChildProcess(backingProcess:System.Diagnostics.Process) = class

    member self.kill = 
        ()
end


// This would've been a module.. but modules don't support overloading OR optional parameters. Lame.
type child_process = class
    
    new() = {}

    member self.exec(command:string, ?callback) =  // TODO: callback
        let p = Process.Start(command)
        new ChildProcess(p)

    member self.exec(command:string, options, ?callback) = // TODO: options, callback
        let p = Process.Start(command)
        new ChildProcess(p)

end
