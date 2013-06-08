namespace Node.Examples
open node

module Main =
        
        [<EntryPoint>]
        let main args =
            node.start(fun x -> staticFileServer.run)
            0
         