module Test.``netServer - listen``

open Node.fs.Core.net
open Xunit
open FsUnit.Xunit
open System.Linq

type ``Given an instance of netServer`` ()=

    let net = new net()
    let netServer = net.createServer(fun socket -> ())
 
    [<Fact>]
    let ``listen(9999) listens on port 9999`` ()=
        netServer.listen(9999)
        let listeners = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners()
        let theListener = listeners.SingleOrDefault(fun l -> l.Port = 9999)
        theListener |> should not' (be null)


    interface System.IDisposable with
        member x.Dispose() = 
            ()