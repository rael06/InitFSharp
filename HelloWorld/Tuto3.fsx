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

let add_10 x = x + 10
let doubleOperation op = op >> op

printfn "%d" <| doubleOperation add_10 2
//
//let add x y = x + y
//let doubleOperationTuple op x y = op <| op x y <| op x y
//
//printfn "%d" <| doubleOperationTuple add 3 5

//let arr = [ 1; 2; 3; 4 ]
//let mapToSquare = (fun x -> x * x) |> List.map
//let displayMapToSquare = mapToSquare >> printfn "%A"
//
//arr |> displayMapToSquare

