﻿module XmasTypes
type Coord = int*int
type Square = Coord*Coord
type Command = 
    | Off of Square
    | On of Square
    | Toggle of Square

type Row = seq<char>
type Grid = seq<Row>

let lightOn = '*'
let lightOff = '.'

let width = 11
let height = 7
let blankRow : Row = (Array.init<char> width (fun _ -> lightOn) |> Array.toSeq)
let blankGrid : Grid = (Array.init<seq<char>> height (fun _ -> blankRow) |> Array.toSeq)


