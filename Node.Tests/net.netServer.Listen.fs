module Test.``netServer - listen``

open Node.net
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
            netServer.close



type ``Given a listening instance of netServer`` ()=

    let net = new net()
    let netServer = net.createServer(fun socket -> ())
    let x = netServer.listen(9999)

    [<Fact>]
    let ``close() closes the listener`` ()= // TODO - async
        netServer.close
        let listeners = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners()
        let theListener = listeners.SingleOrDefault(fun l -> l.Port = 9999)
        theListener |> should be Null

