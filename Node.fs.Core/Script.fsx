// Learn more about F# at http://fsharp.net. See the 'F# Tutorial' project
// for more guidance on F# programming.

#load "console.fs"
#load "http.fs"

open Node.fs
open Node.fs.Core.Http
open Node.fs.Core.Console

// Define your library scripting code here

let handler(req:httpServerRequest, resp:httpServerResponse) =
    resp.writeHead(200, dict["Content-Type", "text/html"])
    resp.write("Hello, world!")
    resp.endResponse

let server = httpServer(handler)

server.listen(9999)
