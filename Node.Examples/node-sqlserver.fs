module node_sqlserver

open node
open Node.Libraries

let run = 
    let sql = require<sqlserver>
    let connectionString = "Data Source=.\\sqlexpress;Initial Catalog=master;Integrated Security=SSPI;"
    sql.openconnection(connectionString, fun(err, conn) ->

        conn.query(connectionString,  fun(err, data) ->

            for row in data do
                let fmt = System.String.Join(", ", row)
                printfn "%s" fmt 

        , "select * from sys.databases") |> ignore
    ) |> ignore


  



