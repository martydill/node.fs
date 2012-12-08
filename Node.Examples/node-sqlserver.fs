module node_sqlserver

open node
open Node.Libraries

let run = 
    let sql = require<sqlserver>
    let connectionString = "Data Source=.\\sqlexpress;Initial Catalog=master;Integrated Security=SSPI;"
    sql.openconnection(connectionString, fun(err, conn) ->

        conn.query(connectionString, "select * from sys.databases where name = ?", [|"master"|] , fun(err, data) ->

            for row:System.Object[] in data do
                let fmt = System.String.Join(", ", row)
                printfn "%s" fmt 
       )
    ) |> ignore



  



