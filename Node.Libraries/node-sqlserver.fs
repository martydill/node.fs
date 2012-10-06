namespace Node.Libraries
open System.Data.SqlClient
open System

type sql_data_row(values) = class

end

type sql_result(results:sql_data_row[]) = class
    
    member self.length = results.Length

end

type sqlserver_connection(_conn) = class

end

// F# version of node-sqlserver
// Based on https://github.com/WindowsAzure/node-sqlserver/blob/master/lib/sql.js


type sqlserver() = class
    inherit Node.emitter.emitter()

    let _conn = new SqlConnection()

    member self.openconnection(connectionString, callback) = // NAMING: should be open

        System.Threading.Tasks.Task.Factory.StartNew(fun () ->
            _conn.ConnectionString <- connectionString
            _conn.Open()

            callback(null, new sqlserver_connection(_conn))
        )

    member self.query(connectionString, callback, query, [<ParamArray>] args: Object[]) = 
        self.queryRaw(connectionString, callback, query, args)
    
    member self.queryRaw(connectionString, callback, query, [<ParamArray>] args: Object[]) = 
        use conn = new SqlConnection(connectionString)
        conn.Open()
        use cmd = new SqlCommand(query, conn)

//        for i in 0 .. args.Length - 1 do
//            cmd.Parameters.AddWithValue("{" + i.ToString() + "}", args.[i]) |> ignore
        

        let allRows = new System.Collections.Generic.List<Object[]>()

        use reader = cmd.ExecuteReader()
        while reader.Read() do
            let rowValues:Object array = Array.zeroCreate reader.FieldCount
            reader.GetValues(rowValues) |> ignore
            allRows.Add(rowValues)
        
       // let callback = args.[args.Length - 1] :?> FSharpFunc<System.Tuple<Object, Object[][]>, unit>
       // callback.Invoke(new Tuple<Object, Object[][]>(null, allRows.ToArray()))
        callback(null, allRows.ToArray())
end

