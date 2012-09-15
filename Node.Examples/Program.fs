namespace Node.Examples

module Main =
        
        [<EntryPoint>]
        let main args =
            tcpDemo.run
            System.Console.ReadLine() |> ignore
            0

         