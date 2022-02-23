// Q1
// Le currying : c'est le fait d'avoir une fonction qui renvoie une autre fonction
// L'higher-order function : c'est le fait de pouvoir appeller une fonction dans une fonction et donc ça permet le currying
// Le partial application : c'est le fait de pouvoir appeler une fonction sans forcément tous les paramètres,
// la fonction ainsi appelée renverra une autre fonction pour chaque paramètre et ainsi de suite.
// On applique ainsi une fonction partiellement.

// Q2
// Une solution consisterait à composer nos types:

//type Animal = { size: float; weight: float }
//
//type Dog = { name: string; animal: Animal }
//
//type Bird = { name: string; animal: Animal }
//
//let doggy: Dog =
//    { name = "doggy"
//      animal = { size = 0.70; weight = 40 } }
//
//let birdy: Bird =
//    { name = "birdy"
//      animal = { size = 0.20; weight = 2 } }
//
//printfn $"%A{doggy}"
//printfn $"%A{birdy}"


// TP
let find list =
    let rec loop acc list =
        match list with
        | [] | _ :: [] -> acc
        | h :: t -> loop (min (t[0]-h) acc) t

    let sortedList = list |> List.sort
    loop (sortedList[1] - sortedList[0]) sortedList

[ 5; 15; 17; 3; 8; 11; 28; 6; 55; 7 ]
|> find
|> printfn "%i"

//let find acc list =
//    match list with
//    | list when List.length list = 1 -> acc
//    | h :: t ->
//        if (acc > 0 && t.[0] - h > acc) then
//            acc
//        else
//            (t.[0] - h)
//    | _ -> acc
//
//[ 5; 15; 17; 3; 8; 11; 28; 6; 55; 7 ]
//|> List.fold find 0
//|> printfn "%i"

