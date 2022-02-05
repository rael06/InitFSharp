
let upperCases = [ 'A' .. 'Z' ]
let lowerCases = [ 'a' .. 'z' ]
let numbers = [ '0' .. '9' ]
let specialChars = [ ','; '.'; '$'; '&'; '@'; '-' ]

let strToChars (str: string) =
    let rec loop index acc =
        match index with
        | i when i = str.Length -> acc
        | _ -> loop (index + 1) (acc @ [ str.[index] ])

    loop 0 []

let contains char list =
    let rec loop char list =
        match list with
        | [] -> false
        | h :: _ when h = char -> true
        | _ :: t -> loop char t

    loop char list

let test3 = contains 'p' specialChars

let isValidChars chars validator =
    let rec loop list isValid =
        match list, isValid with
        | [], _ -> isValid
        | _, true -> true
        | h :: t, _ -> loop t (contains h validator)

    loop chars false

let validate input =
    (strToChars input).Length > 8
    && isValidChars (strToChars input) lowerCases
    && isValidChars (strToChars input) upperCases
    && isValidChars (strToChars input) numbers
    && isValidChars (strToChars input) specialChars

validate "Password1234."
