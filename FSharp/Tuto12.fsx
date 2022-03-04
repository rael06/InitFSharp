open System

let random = Random()

type Rectangle =
    { width: float
      length: float
      getSize: Rectangle -> float }

type Circle =
    { radius: float
      getSize: Circle -> float }

type Shape =
    | Rectangle of rectangle: Rectangle
    | Circle of circle: Circle

let rectangle =
    Rectangle(
        { width = 1.0
          length = 2.5
          getSize = fun { width = w; length = l } -> w * l }
    )

let circle =
    Circle(
        { radius = 2.5
          getSize = fun { radius = r } -> r * 2. * 3.14 }
    )

let calcArea shape =
    match shape with
    | Rectangle r -> r.getSize r
    | Circle c -> c.getSize c

let shapes: List<Shape> = [ rectangle; circle ]
shapes |> List.map calcArea |> printfn "%A"
