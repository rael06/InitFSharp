open System

let specialChars = [ ','; '.'; '$'; '&'; '@'; '-' ]

let (|Uppercase|Lowercase|Number|SpecialChar|Other|) c =
    match c with
    | c when Char.IsUpper c = true -> Uppercase
    | c when Char.IsLower c = true -> Lowercase
    | c when Char.IsDigit c = true -> Number
    | c when specialChars |> List.contains c = true -> SpecialChar
    | _ -> Other

let isValid list =
    let validate (uppercase, lowercase, number, specialChar) =
        function
        | Uppercase -> (true, lowercase, number, specialChar)
        | Lowercase -> (uppercase, true, number, specialChar)
        | Number -> (uppercase, lowercase, true, specialChar)
        | SpecialChar -> (uppercase, lowercase, number, true)
        | Other -> (false, false, false, false)

    if (List.length list < 8) then
        "Not enough chars"
    else
        list
        |> List.fold validate (false, false, false, false)
        |> function
            | false, _, _, _ -> "Missing uppercase"
            | _, false, _, _ -> "Missing lowercase"
            | _, _, false, _ -> "Missing number"
            | _, _, _, false -> "Missing special char"
            | _ -> "Password is valid"

Seq.toList "Password1234."
|> isValid
|> printfn "------\nPassword validation result: \n%s\n------"
