namespace Node.Examples
open node

module Main =
        
        [<EntryPoint>]
        let main args =
            node.start(fun x -> node_sqlserver.run)
            0
         