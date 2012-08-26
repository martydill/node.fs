module Program

open Node.fs.Core.Http
open Node.fs.Core.Console
open Node.fs.Core.Url

module Main =
    open System
        [<EntryPoint>]
        let main args =
            
            
            let http = new http()
            let console = new console()
            let url = new url()

            http.createServer(fun (request, response) -> 
                response.writeHead (200, dict["Content-Type", "text/html"])
                let pathname = url.parse(request.url).pathname
                console.log "Request for %s received." pathname

                if pathname = "/upload" then
                    
                    request.addListener("data", fun data -> 
                        let message = sprintf "You typed %s" data 
                        response.write(message)
                    ) 

                    request.addListener("end", fun data -> 
                        response.endResponse
                    )

                else

                    let data =  "<html>" +
                                "<head>"+
                                "</head>"+
                                "<body>"+
                                "<form action=\"/upload\" method=\"post\">"+
                                "<textarea name=\"text\" rows=\"20\" cols=\"60\"></textarea>"+
                                "<input type=\"submit\" value=\"Submit text\" />"+
                                "</form>"+
                                "</body>"+
                                "</html>"

                    response.write data
                    response.endResponse

            ).listen(8888)

 
            console.log "The server has started" |> ignore
            Console.ReadLine() |> ignore
            0