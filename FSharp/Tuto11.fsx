type Shape =
    | Rectangle of width: float * length: float
    | Circle of radius: float
    | Square of width: float

let shapes = [Rectangle(2.5, 2); Circle(1.8); Square(1.5)]

let calcArea shape =
    match shape with
    | Rectangle(width, length) -> width * length
    | Circle radius -> radius * 2. * 3.14
    | Square width -> width * width

shapes |> List.map calcArea |> printfn "%A"
