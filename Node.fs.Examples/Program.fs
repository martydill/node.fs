
open httpDemo

module Main =
    open System
        [<EntryPoint>]
        let main args =
            httpDemo.run
            Console.ReadLine() |> ignore
            0

         