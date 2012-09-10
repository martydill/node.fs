module webtail

open Node.Http
open Node.child_process

// https://github.com/Leveton/nodetuts/blob/master/episode_2/webtail.js

let run = 

    let http = new http()
    let exec = new child_process()

    http.createServer(fun (request, response) ->
        ()
//	        response.writeHead(200, {
//	          'Content-Type': 'text/plain'
//	        });
//
//	        var tail_child = exec('tail -f var/log/syslog');
//
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
       



