namespace Node.fs.Core.Console

open System
open System.Collections.Generic

exception AssertionError of string

// http://nodejs.org/api/stdio.html
type console = class
   
    new() = { }

    [<DefaultValue>]
    static val mutable private MyProperty : Dictionary<string, DateTime>

    static member log (format : Printf.TextWriterFormat<'T>) =
        printfn format

    static member info (format : Printf.TextWriterFormat<'T>) =
        console.log format

    static member error (format : Printf.TextWriterFormat<'T>) =
        eprintfn format

    static member warn (format : Printf.TextWriterFormat<'T>) =
        console.warn format

    // Remove first two lines of stack trace?
    static member trace (label) = 
        printfn "%s\n%s" label System.Environment.StackTrace

    static member assertThat (expr, ?message) = 

        let msg = match message with
                    | Some x -> x
                    | None -> ""
        
        if not expr then raise(AssertionError(msg))
        ()

    static member time (label) =

        let dict = match console.MyProperty with
                    | null -> new Dictionary<string, DateTime>()
                    | _ -> console.MyProperty
        

        console.MyProperty <- dict
        console.MyProperty.Add(label, DateTime.Now)

    static member timeEnd (label) = 

        let dict = match console.MyProperty with
                    | null -> new Dictionary<string, DateTime>()
                    | _ -> console.MyProperty

        if dict.ContainsKey(label) then 
            
            let endTime = DateTime.Now
            let startTime = dict.[label]

            let diff = endTime - startTime

            console.log "%s: %s" label (diff.ToString())

            dict.Remove(label) |> ignore

    // TODO
    static member dir (obj) = 
        raise (AssertionError("Not implemented"))

end

