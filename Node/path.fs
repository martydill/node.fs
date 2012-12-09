namespace Node.path

// http://nodejs.org/api/path.html
type path = class
    
    new() = {}

    member self.normalize p = 
        System.IO.Path.GetFullPath p

    member self.join([<System.ParamArray>] args: string[]) = 
        let filtered = args |> Seq.filter (fun str -> str <> null) |> Seq.map self.stripInitialSlashes |> Seq.toArray

        System.IO.Path.Combine(filtered)

    member self.resolve([<System.ParamArray>] args: string[]) =
        ()

    member self.relative(from, to_) = // NAMING: Should be to
        ()

    member self.dirname p = 
        System.IO.Path.GetDirectoryName(p)

    member self.basename(p, ?ext:string) = 
        match ext with
        | None -> System.IO.Path.GetFileName p
        | Some string -> 
            match string with
            | s when p.EndsWith(s) ->
                let newPath = System.IO.Path.GetFileName p
                newPath.Substring(0, newPath.Length - s.Length)
            | s -> System.IO.Path.GetFileName p

    member self.extname(p:string) = 
        match p with
        | str when str = null -> ""
        | str when str.EndsWith(".") -> "."
        | str -> System.IO.Path.GetExtension str

    member self.sep = 
        System.IO.Path.DirectorySeparatorChar

    member private self.stripInitialSlashes str:string =
        str.TrimStart([|'\\';'/'|])
   
end
