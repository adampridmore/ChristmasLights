open Xmas1000
open XmasParser

[<EntryPoint>]
let main _ =
    @"XmasCommands.txt" 
    |> System.IO.File.ReadAllText
    |> parseCommands
    |> runCommands 
    |> printGrid

    0
