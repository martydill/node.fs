namespace Node.Examples

open staticFileServer

module Main =
        
        [<EntryPoint>]
        let main args =
            webtail.run
            System.Console.ReadLine() |> ignore
            0

         