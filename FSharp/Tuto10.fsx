open System

let (|Upper|Lower|Number|Punctuation|Other|) c =
    match c with
    | c when Char.IsUpper c -> Upper
    | c when Char.IsLower c -> Lower
    | c when Char.IsNumber c -> Number
    | c when Char.IsPunctuation c -> Punctuation
    | _ -> Other

let categorize =
    function
    | Upper -> "Upper"
    | Lower -> "Lower"
    | Number -> "Number"
    | Punctuation -> "Punctuation"
    | Other -> "Other"

let validate list =
    let rec loop l acc =
        match l, acc with
        | [], _ -> acc
        | h :: t, (_, l, n, s) when h = "Upper" -> loop t (true, l, n, s)
        | h :: t, (u, _, n, s) when h = "Lower" -> loop t (u, true, n, s)
        | h :: t, (u, l, _, s) when h = "Number" -> loop t (u, l, true, s)
        | h :: t, (u, l, n, _) when h = "Punctuation" -> loop t (u, l, n, true)
        | _ -> (false, false, false, false)


    loop list (false, false, false, false)
    |> function
        | false, _, _, _ -> "Missing uppercase"
        | _, false, _, _ -> "Missing lowercase"
        | _, _, false, _ -> "Missing number"
        | _, _, _, false -> "Missing punctuation"
        | _ -> "Password is valid"


"Password1234."
|> Seq.map categorize
|> Seq.toList
|> validate
|> printfn "Validation result: %A"
