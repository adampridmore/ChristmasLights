module Xmas1000
open XmasTypes

type Row = seq<char>
type Grid = seq<Row>

let blankRow = (Array.init<char> 11 (fun _ -> '*') |> Array.toSeq) :> Row
let blankGrid = (Array.init<seq<char>> 7 (fun _ -> blankRow) |> Array.toSeq) :> Grid

let mapij fn grid = 
    grid 
    |> Seq.mapi (fun i row -> row |> Seq.mapi (fun j value -> fn i j value))

let applyCommand command (square:Square) (grid:Grid) : Grid = 
    let (topLeft: Coord),(bottomRight:Coord) = square
    let applyToCell colIndex rowIndex cell =
        let isInBoundingBox = 
            let (tlc, tlr) = topLeft
            let (brc, brr) = bottomRight
            rowIndex >= tlr && rowIndex <= brr &&
            colIndex >= tlc && colIndex <= brc

        if isInBoundingBox 
        then cell |> command
        else cell
            
    grid 
    |> mapij (fun colIndex rowIndex cell -> 
        cell |> applyToCell colIndex rowIndex)

let off = applyCommand (fun _ -> '.')
let on = applyCommand (fun _ -> '*')

let toggle = applyCommand (fun c -> 
    match c with 
    | '*' -> '.'
    | '.' -> '*'
    | _ -> failwith "NO!")

let printGrid grid = 
    let printRow row = 
        row |> Seq.iter (printf "%c")
        printfn ""

    grid |> Seq.iter printRow

let runCommands (commands: seq<Command>) = 
    let mapCommandToFn command = 
        match command with
        | On((a,b),(c,d)) -> on (Square(Coord(a,b),Coord(c,d)))
        | Off((a,b),(c,d)) -> off (Square(Coord(a,b),Coord(c,d)))
       
    let folder (grid : Grid) (command : Grid -> Grid) = 
        grid |> command

    commands 
    |> Seq.map mapCommandToFn
    |> Seq.fold folder blankGrid
    
//type Command = 
//    | On of ((int*int)*(int*int))
//    | Off of ((int*int)*(int*int))
//    | Toggle of ((int*int)*(int*int))

let toSquare (a,b) (c,d) = 
    Square(Coord(a,b),Coord(c,d))

// Shhh - For type inference...
let a() =
    blankGrid 
    |> off (Square(Coord(0,2),(3,4) ) )
    |> on (Square(Coord(0,3),Coord(0,3) ) )
    |> toggle (Square(Coord(1,4),Coord(6,4)))
    |> printGrid
