namespace Node.fs.Core.net

type netServer(requestHandlerFunction:System.Object) = class
     
end

type net = class

    new() = {}

    member self.createServer f = 
        new netServer(f)

end


