let max x y = if x < y then y else x
let min_10 = max 10

6 |> min_10 |> printfn "%d"

let factorial x =
    let rec fact n acc =
        match n with
        | 1 -> acc
        | _ -> fact (n - 1) (n * acc)

    fact x 1

printfn "%d" <| factorial 5
