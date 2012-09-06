module Test.``process``

open node
open Xunit
open FsUnit.Xunit

type ``Given a process`` ()=

    [<Fact>]
    let ``cwd() returns the working directory`` ()=
        proc.cwd |> should equal System.Environment.CurrentDirectory
