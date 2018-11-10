// Learn more about F# at http://fsharp.org

open System
open Xmas1000

[<EntryPoint>]
let main argv =
    
//***********
//*    *    *
//*   ***   *
//*  *****  * 
//* ******* * 
//*    *    * 
//***********

    blankGrid 
    |> off (toSquare (0,0) (7,11))
    |> on (toSquare (0,0) (0,11) )
    |> on (toSquare (6,0) (6,11) )
    |> on (toSquare (1,5) (1,5) )
    |> on (toSquare (2,4) (2,6) )
    |> on (toSquare (3,3) (3,7) )
    |> on (toSquare (4,1) (4,9) )
    |> on (toSquare (5,5) (5,5) )
    |> printGrid

    0 // return an integer exit code
