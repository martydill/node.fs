namespace Node.stream
open System.Linq

type StreamReadOptions = {
    flags: string
    encoding: option<string>
    fd: option<string>
    mode: int
    bufferSize: int
}
//
//{ flags: 'r',
//  encoding: null,
//  fd: null,
//  mode: 0666,
//  bufferSize: 64 * 1024
//}
type BaseStream(stream:System.IO.Stream) as this = class
    inherit Node.emitter.emitter()

    [<Literal>]
    let CloseEvent = "close"

    [<Literal>]
    let ErrorEvent = "error"
    
    [<Literal>]
    let DataEvent = "data"

    [<Literal>]
    let EndEvent = "end"

    do
        
        match stream.CanRead with
        | true -> Async.Start this.StartReadingStream
        | false -> ()

    member self.destroy = 
        stream.Dispose
        

    member self.readable = 
        stream.CanRead

    member private self.StartReadingStream = 
        async {
            let count = ref 1
            try

                while !count > 0 && stream.CanRead do
                    let buffer = Array.zeroCreate<byte>(1024)
                    let! n = stream.AsyncRead(buffer, 0, buffer.Length)
                    if n > 0 then self.emit(DataEvent, buffer.Take(n).ToArray())
                    count := n
            with
                | :? System.IO.IOException -> ()    // raise error event?
                | :? System.ObjectDisposedException -> ()
        }

 end

 type FileStream(path, ?options) = class
    inherit BaseStream(new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate))

 end