namespace Node.Examples

open httpDemo

module Main =
        
        [<EntryPoint>]
        let main args =
            httpDemo.run
            System.Console.ReadLine() |> ignore
            0

         