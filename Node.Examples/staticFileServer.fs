module staticFileServer

// http://www.hongkiat.com/blog/node-js-server-side-javascript/

open Node.Http
open Node.Url
open Node.fs
open Node.sys
open Node.path
open node

let run = 

    let sys = new sys()
    let my_http = new http()
    let path = new path()
    let url = new url()
    let filesys = new fs()

    my_http.createServer(fun (request,response) ->
        
        let my_path = url.parse(request.url).pathname
        let full_path = path.join(proc.cwd, my_path)
        ()

//	    path.exists(full_path,function(exists){
//		    if(!exists){
//			    response.writeHeader(404, {"Content-Type": "text/plain"})
//			    response.write("404 Not Found\n")
//			    response.end()
//		    }
//		    else{
//			    filesys.readFile(full_path, "binary", function(err, file) {
//			         if(err) {
//			             response.writeHeader(500, {"Content-Type": "text/plain"})
//			             response.write(err + "\n")
//			             response.end()  
//
//			         }
//				     else{
//					    response.writeHeader(200)
//			            response.write(file, "binary")
//			            response.end()
//				    }
//
//			    })
//	    )
    ).listen(8080)
    //sys.puts("Server Running on 8080")
