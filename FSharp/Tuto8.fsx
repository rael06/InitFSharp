open System

let specialChars = [ ','; '.'; '$'; '&'; '@'; '-' ]

let (|Uppercase|Lowercase|Number|SpecialChar|Other|) c =
    match c with
    | c when Char.IsUpper c = true -> Uppercase
    | c when Char.IsLower c = true -> Lowercase
    | c when Char.IsDigit c -> Number
    | c when specialChars |> List.contains c = true -> SpecialChar
    | _ -> Other

type CharType =
    | Uppercase = 0
    | Lowercase = 1
    | Number = 2
    | SpecialChar = 3
    | Other = 4

let validateChar =
    function
    | Uppercase -> CharType.Uppercase
    | Lowercase -> CharType.Lowercase
    | Number -> CharType.Number
    | SpecialChar -> CharType.SpecialChar
    | Other -> CharType.Other

let chars = Seq.toList "Password1234" |> List.map validateChar
let isNumberValid = chars |> List.contains CharType.Number
let isUppercaseValid = chars |> List.contains CharType.Uppercase
let isLowercaseValid = chars |> List.contains CharType.Lowercase
let isSpecialCharValid = chars |> List.contains CharType.SpecialChar
let isLengthValid = chars |> List.length >= 8

printfn $"Is password valid: %A{isNumberValid && isUppercaseValid && isLowercaseValid && isSpecialCharValid && isLengthValid}"
