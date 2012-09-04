namespace Node.Examples

open tcpChat

module Main =
    open System
        [<EntryPoint>]
        let main args =
            tcpChat.run
            Console.ReadLine() |> ignore
            0

         