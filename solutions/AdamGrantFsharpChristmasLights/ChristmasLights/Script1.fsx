#r @"C:\Users\adam_\.nuget\packages\fparsec\1.0.3\lib\net40-client\FParsecCS.dll"
#r @"C:\Users\adam_\.nuget\packages\fparsec\1.0.3\lib\net40-client\FParsec.dll"

open FParsec

let test p str =
    match run p str with
    | Success(result, _, _)   -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

type Coord = int*int
type Square = Coord*Coord
type Cmd = 
    | Off of Square
    | On of Square

let str s = pstring s
let betweenStrings s1 s2 p = str s1 >>. p .>> str s2
//let floatBetweenBrackets = pfloat |> betweenStrings "[" "]"

let pcoord = tuple2 (pint32 .>> pchar ',') pint32 |>> Coord
test pcoord "1,2"

let pbracketcoord = betweenStrings "(" ")" pcoord
test pbracketcoord "(1,2)"

let psquare = pipe2 pbracketcoord pbracketcoord (fun a b -> Square(a,b))
test psquare "(1,2)(3,4)"

let poff = pstring "OFF " >>. psquare |>> Off
test poff "OFF (1,2)(3,4)"

let pon = pstring "ON " >>. psquare |>> On
test pon "ON (1,2)(3,4)"

let pcommand = spaces >>. (pon <|> poff)
test pcommand " 
ON (1,2)(3,4)"
test pcommand " OFF (1,2)(3,4)"

let pmanycommands = many pcommand
test pmanycommands @"OFF (1,2)(3,4)
ON (5,6)(7,8)"
