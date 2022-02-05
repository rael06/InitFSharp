let alphabet = ['A'..'G'] |> Set.ofList
let vowels = Set.empty.Add('A').Add('E').Add('I').Add('O').Add('U').Add('Y')
let union = Set.union alphabet vowels
let uniques = Set.union alphabet vowels
let intersect = Set.intersect alphabet vowels
let difference = Set.difference alphabet vowels
let difference2 = alphabet - vowels
