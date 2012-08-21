// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "console.fs"
#load "http.fs"

open Node.fs.Core.Http
open Node.fs.Core.Console

let http = new http()
let console = new console()

http.createServer(fun (request, response) -> 
    response.writeHead (200, dict["Content-Type", "text/plain"])
    response.write "Hello, world!"
    response.endResponse
).listen(8888)

 
console.log "The server has started"