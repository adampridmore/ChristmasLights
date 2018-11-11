module XmasTypes

type Cell = char
type Row = seq<Cell>
type Grid = seq<Row>

let lightOn = '*'
let lightOff = '.'

let width = 11
let height = 7

type Coord = int*int
type Square = Coord*Coord

type Command = 
    | Off of Square
    | On of Square
    | Toggle of Square

let blankRow : Row = 
    Array.init<Cell> width (fun _ -> lightOn) 
    |> Array.toSeq

let blankGrid : Grid = 
    Array.init<Row> height (fun _ -> blankRow) 
    |> Array.toSeq
