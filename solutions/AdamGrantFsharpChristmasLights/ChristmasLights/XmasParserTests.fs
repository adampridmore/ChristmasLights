module XmasParserTests

open Xunit
open XmasTypes
open XmasParser
open FParsec

[<Fact>]
let ``parse coord``() = 
    let actual = parseParserCommand pcoord "1,2"
    Assert.Equal( Coord(1,2), actual)

[<Fact>]
let ``parse braceted coord``() = 
    let actual = parseParserCommand pbracketcoord "(1,2)"
    Assert.Equal( Coord(1,2), actual)

[<Fact>]
let ``parse square``() = 
    let actual = parseParserCommand psquare  "(1,2)(3,4)"
    Assert.Equal( Square(Coord(1,2),Coord(3,4)) , actual)

[<Fact>]
let ``parse off``() = 
    let actual = parseParserCommand poff "OFF (1,2)(3,4)"
    Assert.Equal( Off(Square(Coord(1,2),Coord(3,4))) , actual)

[<Fact>]
let ``parse on``() = 
    let actual = parseParserCommand pon "ON (1,2)(3,4)"
    Assert.Equal( On(Square(Coord(1,2),Coord(3,4))) , actual)

[<Fact>]
let ``parse toggle``() = 
    let actual = parseParserCommand ptoggle "TOGGLE (1,2)(3,4)"
    Assert.Equal( Toggle(Square(Coord(1,2),Coord(3,4))) , actual)


[<Fact>]
let ``parse command``() = 
    let actual = parseParserCommand pcommand "ON (1,2)(3,4)"
    Assert.Equal( On(Square(Coord(1,2),Coord(3,4))) , actual)

[<Fact>]
let ``parse many commands``() = 
    let text = @"OFF (1,2)(3,4)
ON (5,6)(7,8)
TOGGLE (9,10,11,12)"
    
    let actual = parseParserCommand pmanycommands text

    let expected = 
        [
            Off(Square(Coord(1,2),Coord(3,4)));
            On(Square(Coord(5,6),Coord(7,8)));
            Toggle(Square(Coord(9,10),Coord(11,12)));
        ] 

    Assert.Equal(3,actual.Length)

    Assert.Equal (expected.[0] , actual.[0])
    Assert.Equal (expected.[1] , actual.[1])
    Assert.Equal (expected.[2] , actual.[2])