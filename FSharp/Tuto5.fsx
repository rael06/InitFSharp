let list = [ 1; 2; 3; 4; 2 ]

let mapi f list =
    let rec loop list index acc =
        match list with
        | [] -> acc
        | head :: tail -> loop tail (index + 1) (acc @ [ f index head ])

    loop list 0 []

let maxBy f l =
    let rec loop f l result =
        match l with
        | [] -> result
        | h :: t ->
            match result with
            | None -> loop f t (Some h)
            | Some r -> loop f t (if (f h) < (f r) then Some r else Some h)

    loop f l None

//maxBy snd (mapi (fun i x -> i, x) list)

let result =
    list |> mapi (fun i x -> i, x) |> maxBy snd

match result with
| Some result -> printfn $"%d{fst result}"
| None -> printfn "error"

let maxIndex =
    mapi (fun i x -> i, x)
    >> maxBy snd
    >> function
        | Some x -> Some(fst x)
        | _ -> None

list |> maxIndex |> printfn "%A"
