namespace Node.Examples

open staticFileServer

module Main =
    open System
        [<EntryPoint>]
        let main args =
            staticFileServer.run
            Console.ReadLine() |> ignore
            0

         