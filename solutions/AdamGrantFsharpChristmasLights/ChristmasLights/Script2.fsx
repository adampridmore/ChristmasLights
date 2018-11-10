#r @"C:\Users\adam_\.nuget\packages\fparsec\1.0.3\lib\net40-client\FParsecCS.dll"
#r @"C:\Users\adam_\.nuget\packages\fparsec\1.0.3\lib\net40-client\FParsec.dll"

open FParsec

let test p str =
    match run p str with
    | Success(result, _, _)   -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

let str s = pstring s

let betweenStrings s1 s2 p = str s1 >>. p .>> str s2

let floatBetweenBrackets = pfloat |> betweenStrings "[" "]"

let myFile = many floatBetweenBrackets

test myFile @""
test myFile @"[1.2]"
test myFile @"[1.2][2.3]"

