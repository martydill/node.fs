namespace Node.util

type util = class
    
    new() = {}

    member self.puts([<System.ParamArray>] args: string[]) = 
        args |> Seq.map (fun arg -> printfn "%s\n" arg) |> ignore

end
