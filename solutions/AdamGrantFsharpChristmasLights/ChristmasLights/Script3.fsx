#r @"C:\Users\adam_\.nuget\packages\fparsec\1.0.3\lib\net40-client\FParsecCS.dll"
#r @"C:\Users\adam_\.nuget\packages\fparsec\1.0.3\lib\net40-client\FParsec.dll"

#load "XmasTypes.fs"
#load "XmasParser.fs"
#load "Xmas1000.fs"

open Xmas1000
open XmasParser

System.IO.Path.Combine(__SOURCE_DIRECTORY__, @"XmasCommands.txt" )
|> System.IO.File.ReadAllText
|> parseCommands
|> runCommands 
|> printGrid

