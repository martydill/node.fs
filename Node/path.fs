namespace Node.path

// http://nodejs.org/api/path.html
type path = class
    
    new() = {}

    member self.normalize p = 
        ()

    member self.join([<System.ParamArray>] args: string[]) = 
        ()

    member self.resolve([<System.ParamArray>] args: string[]) =
        ()

    member self.relative(from, to_) = // NAMING: Should be to
        ()

    member self.dirname p = 
        ()

    member self.basename(p, ?ext) = 
        ()

    member self.extname p = 
        ()

    member self.sep = 
        ()
   
end
