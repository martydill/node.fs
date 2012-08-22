
#load "console.fs"
#load "http.fs"
#load "url.fs"

open Node.fs.Core.Http
open Node.fs.Core.Console
open Node.fs.Core.Url

let http = new http()
let console = new console()
let url = new url()

http.createServer(fun (request, response) -> 
    response.writeHead (200, dict["Content-Type", "text/plain"])
    let pathname = url.parse(request.url).pathname
    console.log "Request for %s received." pathname
    response.write "Hello, world"
    response.endResponse
).listen(8888)

 
console.log "The server has started"