module webtail

open Node.Http
open Node.child_process

// https://github.com/Leveton/nodetuts/blob/master/episode_2/webtail.js

let run = 

    let http = new http()
    let cp = new child_process()

    http.createServer(fun (request, response) ->

        response.writeHead(200, dict["Content-Type", "text/html"])

        let tail_child = cp.exec("tail -f Node.examples.config")
        request.addListener("end", fun error ->
            tail_child.kill
        )

//	        request.connection.on('end', function(){
//              tail_child.kill();
//	        });
//
//	        tail_child.on('data', function(data){
//	          console.log(data.toString());
//	          response.write(data);
//	        });
    ).listen(4000)
    ()
       



