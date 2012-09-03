namespace Node.Console

open System
open System.Collections.Generic

exception AssertionError of string

// http://nodejs.org/api/stdio.html
type console = class
   
    new() = { }

    [<DefaultValue>]
    val mutable private MyProperty : Dictionary<string, DateTime>

    member self.log (format : Printf.TextWriterFormat<'T>) =
        printfn format

    member self.info (format : Printf.TextWriterFormat<'T>) =
        self.log format

    member self.error (format : Printf.TextWriterFormat<'T>) =
        eprintfn format

    member self.warn (format : Printf.TextWriterFormat<'T>) =
        self.warn format

    // Remove first two lines of stack trace?
    member self.trace (label) = 
        printfn "%s\n%s" label System.Environment.StackTrace

    member self.assertThat (expr, ?message) = 

        let msg = match message with
                    | Some x -> x
                    | None -> ""
        
        if not expr then raise(AssertionError(msg))
        ()

    member self.time (label) =

        let dict = match self.MyProperty with
                    | null -> new Dictionary<string, DateTime>()
                    | _ -> self.MyProperty
        

        self.MyProperty <- dict
        self.MyProperty.Add(label, DateTime.Now)

    member self.timeEnd (label) = 

        let dict = match self.MyProperty with
                    | null -> new Dictionary<string, DateTime>()
                    | _ -> self.MyProperty

        if dict.ContainsKey(label) then 
            
            let endTime = DateTime.Now
            let startTime = dict.[label]

            let diff = endTime - startTime

            self.log "%s: %s" label (diff.ToString())

            dict.Remove(label) |> ignore

    // TODO
    static member dir (obj) = 
        raise (AssertionError("Not implemented"))

end

