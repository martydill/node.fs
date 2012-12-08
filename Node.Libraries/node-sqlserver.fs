// F# version of node-sqlserver
// Based on https://github.com/WindowsAzure/node-sqlserver/blob/master/lib/sql.js

namespace Node.Libraries

open System.Data.SqlClient
open System
open System.Linq

type sqlserver_connection(_conn) = class

    static member ParameterReplacementRegex = new System.Text.RegularExpressions.Regex("\?")

    member self.query(connectionString, query, [<ParamArray>] args: Object[], callback) = 
        self.queryRaw(connectionString, query, args, callback)

    member self.queryRaw(connectionString, query:string, [<ParamArray>] args: Object[], callback) = 
         
        let mutable modifiedQuery = query

        use cmd = new SqlCommand()

        // Replace ? parameters with named parameters, in order
        for param in args do
            let parameterNumber = cmd.Parameters.Count
            let parameterName = "@p" + parameterNumber.ToString()
            cmd.Parameters.AddWithValue(parameterName, param) |> ignore
            modifiedQuery <- sqlserver_connection.ParameterReplacementRegex.Replace(modifiedQuery, parameterName, 1) 

        cmd.CommandText <- modifiedQuery
        cmd.Connection <- _conn

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



type sqlserver() = class
    inherit Node.emitter.emitter()

    member self.openconnection(connectionString, callback) = // NAMING: should be open

        System.Threading.Tasks.Task.Factory.StartNew(fun () ->
            let conn = new SqlConnection()
            conn.ConnectionString <- connectionString
            conn.Open()

            callback(null, new sqlserver_connection(conn))
        )

    member self.query(connectionString, query, [<ParamArray>] args: Object[], callback) = 
        self.queryRaw(connectionString, query, args, callback)
    
    member self.queryRaw(connectionString, query, [<ParamArray>] args: Object[], callback) = 
        self.openconnection(connectionString, fun(err, conn) ->
            conn.queryRaw(connectionString, query, args, callback)
        )
end
