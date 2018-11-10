module XmasTypes

type Coord = int*int
type Square = Coord*Coord
type Command = 
    | Off of Square
    | On of Square

