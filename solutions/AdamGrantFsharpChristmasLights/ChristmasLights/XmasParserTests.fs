module XmasParserTests

open Xunit
open XmasParser
open FParsec
open XmasTypes

let parse p str = 
    match run p str with    
    | Success(result, _, _)   -> result
    | Failure(errorMsg, _, _) -> 
        failwith (sprintf "Failure: %s" errorMsg)

[<Fact>]
let ``parse coord``() = 
    let actual = parse pcoord "1,2"
    Assert.Equal( Coord(1,2), actual)

[<Fact>]
let ``parse braceted coord``() = 
    let actual = parse pbracketcoord "(1,2)"
    Assert.Equal( Coord(1,2), actual)

[<Fact>]
let ``parse square``() = 
    let actual = parse psquare  "(1,2)(3,4)"
    Assert.Equal( Square(Coord(1,2),Coord(3,4)) , actual)

[<Fact>]
let ``parse off``() = 
    let actual = parse poff "OFF (1,2)(3,4)"
    Assert.Equal( Off(Square(Coord(1,2),Coord(3,4))) , actual)

[<Fact>]
let ``parse on``() = 
    let actual = parse pon "ON (1,2)(3,4)"
    Assert.Equal( On(Square(Coord(1,2),Coord(3,4))) , actual)

[<Fact>]
let ``parse command``() = 
    let actual = parse pcommand "ON (1,2)(3,4)"
    Assert.Equal( On(Square(Coord(1,2),Coord(3,4))) , actual)


[<Fact>]
let ``parse many commands``() = 
    let text = @"OFF (1,2)(3,4)
ON (5,6)(7,8)"
    
    let actual = (parse pmanycommands text)

    let expected = 
        [
            Off(Square(Coord(1,2),Coord(3,4)));
            On(Square(Coord(5,6),Coord(7,8)));
        ] 

    Assert.Equal (expected.[0] , actual.[0])
    Assert.Equal (expected.[1] , actual.[1])