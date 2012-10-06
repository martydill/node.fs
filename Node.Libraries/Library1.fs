namespace Node.Libraries

// F# version of node-sqlserver
// Based on https://github.com/WindowsAzure/node-sqlserver/blob/master/lib/sql.js
type sqlserver = class
    new() = {}

    member self.openconnection(connectionString, callback) = // NAMING: should be open
        ()

    member self.query(connectionString, query, paramsOrCallback, callback) = 
        ()

    member self.queryRaw(connectionString, query, paramsOrCallback, callback) = 
        (
end