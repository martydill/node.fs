// Based on https://gist.github.com/707146
//
module tcpChat

open node
open Node.net
open Node.Console

type mySocket(socket, name) = class
    member self.name = 
        name
    member self.socket =
        socket
end

let run = 

    // Load the TCP Library
    let net = require<net>
    let console = require<console>

    // Keep track of the chat clients
    let clients = ref List.Empty

    // Start a TCP Server
    net.createServer(fun socket ->

        // Identify this client        
        let socketWrapper = new mySocket(socket, socket.remoteAddress + ":" + socket.remotePort)

        // Put this new client in the list
        clients.Value <- socketWrapper :: clients.Value
        
        // Send a nice welcome message and announce
        socket.write("Welcome " + socketWrapper.name + "\n");

        // Send a message to all clients
        let broadcast(message, sender) =
            clients.Value 
                |> List.filter (fun i -> i <> sender)
                |> List.map (fun i -> i.socket.write message)
                |> ignore

        broadcast(socketWrapper.name + " joined the chat\n", socketWrapper)

        // Handle incoming messages from clients.
        socket.addListener("data", fun data ->
            broadcast(socketWrapper.name + "> " + (data.ToString()), socketWrapper)
        )

        // Remove the client from the list when it leaves
        socket.addListener("end", fun data -> 
            clients.Value <- clients.Value |> List.filter(fun x -> x <> socketWrapper)
            broadcast(socketWrapper.name + " left the chat.\n", socketWrapper)
        )

//        // Log it to the server output too
//        process.stdout.write(message) TODO - not implemented
//      }

    ).listen(5000)

    // Put a friendly message on the terminal of the server.
    console.log("Chat server running at port 5000\n")