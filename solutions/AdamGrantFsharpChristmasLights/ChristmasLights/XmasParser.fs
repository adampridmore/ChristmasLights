﻿module XmasParser

open FParsec
open XmasTypes

let str s = pstring s
let betweenStrings s1 s2 p = str s1 >>. p .>> str s2
let pcoord : Parser<Coord, unit> = tuple2 (pint32 .>> pchar ',') pint32 |>> Coord
let pbracketcoord = betweenStrings "(" ")" pcoord
let psquare = pipe2 pbracketcoord pbracketcoord (fun a b -> Square(a,b))
let poff = pstring "OFF " >>. psquare |>> Off
let pon = pstring "ON " >>. psquare |>> On
let pcommand = spaces >>. (pon <|> poff)
let pmanycommands = many pcommand
