module Test.``net - createServer``

open Node.net
open Xunit
open FsUnit.Xunit

type ``Given an instance of net`` ()=

    let net = new net()
 
    [<Fact>]
    let ``createServer creates a server instance`` ()=
      let server = net.createServer(fun socket -> ())
      server |> should be ofExactType<netServer>
  