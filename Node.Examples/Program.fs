namespace Node.Examples

open tcpDemo

module Main =
    open System
        [<EntryPoint>]
        let main args =
            tcpDemo.run
            Console.ReadLine() |> ignore
            0

         