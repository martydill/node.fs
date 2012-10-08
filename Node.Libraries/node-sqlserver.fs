namespace Node.Libraries

open System.Data.SqlClient
open System

type sqlserver_connection(_conn) = class

    member self.query(connectionString, callback, query, [<ParamArray>] args: Object[]) = 
        self.queryRaw(connectionString, callback, query, args)

    member self.queryRaw(connectionString, callback, query, [<ParamArray>] args: Object[]) = 
             
        use cmd = new SqlCommand(query, _conn)

//        for i in 0 .. args.Length - 1 do
//            cmd.Parameters.AddWithValue("{" + i.ToString() + "}", args.[i]) |> ignore
        
        let allRows = new System.Collections.Generic.List<Object[]>()

        use reader = cmd.ExecuteReader()
        while reader.Read() do
            let rowValues:Object array = Array.zeroCreate reader.FieldCount
            reader.GetValues(rowValues) |> ignore
            allRows.Add(rowValues)

        callback(null, allRows.ToArray())
        
         
    interface System.IDisposable with
        member x.Dispose() = 
            _conn.Dispose()

end

// F# version of node-sqlserver
// Based on https://github.com/WindowsAzure/node-sqlserver/blob/master/lib/sql.js


type sqlserver() = class
    inherit Node.emitter.emitter()

    member self.openconnection(connectionString, callback) = // NAMING: should be open

        System.Threading.Tasks.Task.Factory.StartNew(fun () ->
            let conn = new SqlConnection()
            conn.ConnectionString <- connectionString
            conn.Open()

            callback(null, new sqlserver_connection(conn))
        )

    member self.query(connectionString, callback, query, [<ParamArray>] args: Object[]) = 
        self.queryRaw(connectionString, callback, query, args)
    
    member self.queryRaw(connectionString, callback, query, [<ParamArray>] args: Object[]) = 
        self.openconnection(connectionString, fun(err, conn) ->
            conn.queryRaw(connectionString, callback, query, args)
        )
        
end

