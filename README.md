Node.fs is an F# implementation of the Node.js platform
============================================
It is still in extremely early development - so early, in fact, that it can't do much of anything yet, and has no real module system.
I aim to keep the API as similar as possible to the Node.js APIs, but due to language differences (and the abundance of reserved words in F#) some things may be slightly different, or be named slightly differently.

It can currently run F# versions of simple Node.js apps, such as some of the ones from the Node Beginner Book (http://www.nodebeginner.org).
Here is an example:
<pre>
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
</pre>


