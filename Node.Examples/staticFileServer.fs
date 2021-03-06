﻿module staticFileServer

// http://www.hongkiat.com/blog/node-js-server-side-javascript/

open Node.Http
open Node.Url
open Node.fs
open Node.util
open Node.path
open node

let run = 

    let util = require<util>
    let my_http = require<http>
    let path = require<path>
    let url = require<url>
    let filesys = require<fs>

    my_http.createServer(fun (request, response) ->
            
        let my_path = url.parse(request.url).pathname
        let full_path = path.join(proc.cwd, my_path)

        filesys.exists(full_path, fun exists ->
            
            if not exists then
                response.writeHead(404, dict["Content-Type", "text/plain"])
                response.write("404 Not Found\n")
                response.endResponse
        
            else

                filesys.readFile(full_path, fun(bytes, error) -> // TODO  - error parameter
                    let err = null
                    if err <> null then
                        response.writeHead(500, dict["Content-Type", "text/plain"])
                        response.write(err + "\n")
                        response.endResponse
    
                    else
                        response.writeHead(200)
                        response.write(bytes)
                        response.endResponse
                ) |> ignore
        ) |> ignore

    ).listen(8080)

    util.puts("Server Running on 8080")
