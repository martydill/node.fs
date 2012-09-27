module Test.``child_process``

open node
open Node.child_process
open Xunit
open FsUnit.Xunit

type ``Given a ChildProces object`` ()=

    let cp = require<child_process>

    [<Fact>]
    let ``kill kills the process`` ()=
        let childProcess = cp.exec "tail"
        let id = childProcess.pid

        let dotNetProcess = System.Diagnostics.Process.GetProcessById(id)
        childProcess.kill
        dotNetProcess.WaitForExit(100) |> should equal true

