module Xmas1000
open XmasTypes

let private mapij fn grid = 
    grid 
    |> Seq.mapi (fun i row -> 
        row 
        |> Seq.mapi (fun j value -> fn i j value)
    )

let private applyCommand command (square:Square) (grid:Grid) : Grid = 
    let (tlc, tlr),(brc, brr) = square

    let applyToCell colIndex rowIndex cell =
        let isInBoundingBox = 
            rowIndex >= tlr && rowIndex <= brr &&
            colIndex >= tlc && colIndex <= brc

        if isInBoundingBox 
        then cell |> command
        else cell
            
    grid 
    |> mapij (fun colIndex rowIndex cell -> 
        cell |> applyToCell colIndex rowIndex)

let private off = applyCommand (fun _ -> '.')
let private on = applyCommand (fun _ -> '*')
let private toggle = 
    applyCommand (function 
        | light when light = lightOn -> lightOff
        | light when light = lightOff -> lightOn
        | _ -> failwith "NO!")

let printGrid grid = 
    let printRow row = 
        row |> Seq.iter (printf "%c")
        printfn ""

    grid |> Seq.iter printRow

let runCommands (commands: seq<Command>) = 
    let mapCommandToFn command = 
        match command with
        | On(square) -> on square
        | Off(square) -> off square
        | Toggle(square) -> toggle square
        
    let folder (grid : Grid) (command : Grid -> Grid) = 
        grid |> command

    commands 
    |> Seq.map mapCommandToFn
    |> Seq.fold folder blankGrid
    
let toSquare (a,b) (c,d) = 
    Square(Coord(a,b),Coord(c,d))

// Shhh - For type inference...
let a() =
    blankGrid 
    |> off (Square(Coord(0,2),(3,4) ) )
    |> on (Square(Coord(0,3),Coord(0,3) ) )
    |> toggle (Square(Coord(1,4),Coord(6,4)))
    |> printGrid
