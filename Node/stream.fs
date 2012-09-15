namespace Node.stream

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
type BaseStream() = class end

    
type ReadableStream(path, ?options:StreamReadOptions) = class
    inherit BaseStream()

    let _fs = new System.IO.FileStream(path, System.IO.FileMode.Open)

    member self.destroy() = 
        _fs.Dispose()

    member self.readable = 
        _fs.CanRead

end


type WritableStream() = class
    inherit BaseStream()

end
