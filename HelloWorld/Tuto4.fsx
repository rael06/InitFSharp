let inline add x y = x + y
let concat x y = $"%s{x}%s{y}"
let list1 = [ 1; 2; 3; 4 ]
let list2 = [ 5; 6; 7; 8 ]
let list3 = [ "1"; "2"; "3"; "4" ]
let list4 = [ "5"; "6"; "7"; "8" ]
let list5 = [ 1.2; 2.2; 3.2; 4.2 ]
let list6 = [ 5.7; 6.7; 7.7; 8.7 ]

let zip f list1 list2 =
    let rec loop list1 list2 acc =
        match list1, list2 with
        | _, [] -> acc
        | [], _ -> acc
        | head1 :: tail1, head2 :: tail2 -> loop tail1 tail2 (acc @ [ f head1 head2 ])

    loop list1 list2 []

(zip add list1 list2) |> printfn "%A"
(zip add list5 list6) |> printfn "%A"
(zip (fun x y -> x * y) list1 list2) |> printfn "%A"
(zip concat list3 list4) |> printfn "%A"
