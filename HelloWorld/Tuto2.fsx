let add_10 x = x + 10
let doubleOperation op = op >> op

printfn "%d" <| doubleOperation add_10 2
