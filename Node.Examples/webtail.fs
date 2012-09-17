module webtail

open node
open Node.Http
open Node.child_process
open Node.Console

// https://github.com/Leveton/nodetuts/blob/master/episode_2/webtail.js

let run = 

    let http = require<http>
    let cp = require<child_process>
    let console = require<console>

    http.createServer(fun (request, response) ->

        response.writeHead(200, dict["Content-Type", "text/html"])

        let tail_child = cp.exec("tail -f Node.examples.config")
        request.addListener("end", fun error ->
            tail_child.kill
        )

//
//	        tail_child.on('data', function(data){
//	          console.log(data.toString());
//	          response.write(data);
//	        });
    ).listen(4000)
    ()
       



