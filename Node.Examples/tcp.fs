module tcpDemo

open Node.net
open Node.Console

// http://howtonode.org/hello-node

let run = 

    // Load the net module to create a tcp server.
    let net = new net()
    let console = new console()

    // Setup a tcp server
    let server = net.createServer(fun socket ->

        // Every time someone connects, tell them hello and then close the connection.
        console.log "Connection from %s" socket.remoteAddress
        socket.endSocket("Hello World\n")
    )

    // Fire up the server bound to port 7000 on localhost
    server.listen(7000, "localhost")

    // Put a friendly message on the terminal
    console.log("TCP server listening on port 7000 at localhost.")