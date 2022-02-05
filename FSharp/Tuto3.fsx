let add x y = x + y
let doubleOperationTuple op x y = op <| op x y <| op x y

printfn "%d" <| doubleOperationTuple add 3 5

let arr = [ 1; 2; 3; 4 ]
let mapToSquare = (fun x -> x * x) |> List.map
let displayMapToSquare = mapToSquare >> printfn "%A"

arr |> displayMapToSquare

