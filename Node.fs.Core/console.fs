
exception AssertionError of string

// http://nodejs.org/api/stdio.html

type console = class

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

end

