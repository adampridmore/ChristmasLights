open System
open Xmas1000
open XmasParser

[<EntryPoint>]
let main argv =

    @"XmasCommands.txt" 
    |> System.IO.File.ReadAllText
    |> parseCommands
    |> runCommands 
    |> printGrid

    0
