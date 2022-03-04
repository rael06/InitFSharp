// Question 1:
// On peut utiliser le discriminated union comme alternative à l'héritage, en effet en définissant un 'OR' type 'Parent'
// (parent est clairement un abus mais c'est juste pour le rapporter au concept POO, le OR type n'est pas parent), en matchant dessus on
// retrouve les types 'Enfants', pareil c'est un abus. Mon parent peut être n'importe lequel des types enfants:

// type Square = {
//     size: int
// }

// type Circle = {
//     radius: int
// }

// type Shape =
//     | Square of Square
//     | Circle of Circle

// En manipulant un type Shape, je manipule soit un type Square soit un type Circle, je manipule ainsi bien deux types différents au travers d'un seul.




// Question 2:
// Le MailboxProcessor est un agent permettant une communication entre deux processus asynchrones.





// Exercice 1:
// #r @"nuget: FSharp.Data"
// #r @"nuget: XPlot.GoogleCharts"

// open FSharp.Data
// open XPlot.GoogleCharts
// open System

// [<Literal>]
// let ResolutionFolder = __SOURCE_DIRECTORY__

// type Stocks = CsvProvider<"MSFT.csv", ResolutionFolder=ResolutionFolder>

// type StockInfo =
//     { date: DateTime
//       openPrice: decimal
//       closePrice: decimal
//       volume: int }

// let stocks =
//     Stocks
//         .Load(
//             __SOURCE_DIRECTORY__ + "/MSFT.csv"
//         )
//         .Cache()
//         .Rows

// let stockInfos =
//     stocks
//     |> Seq.map (fun x ->
//         { StockInfo.date = x.Date
//           openPrice = x.Open
//           closePrice = x.Close
//           volume = x.Volume })

// stockInfos |> Seq.iter (printfn "%A")

// let closePricesByDates =
//     stockInfos
//     |> Seq.map (fun x -> (x.date, x.closePrice))

// let options =
//     Options(title = "Stocks", curveType = "function", legend = Legend(position = "bottom"))

// let chart =
//     [ closePricesByDates ]
//     |> Chart.Line
//     |> Chart.WithOptions options
//     |> Chart.WithLabels [ "Dates"
//                           "Close prices" ]

// chart.Show()


// Exercice 2:
// module UserModule =

open System.Text.RegularExpressions

type Id = Id of int
type Email = Email of string

type UserError = { field: string; error: string }

type User = { id: Id; email: Email }

let validateId id =
    if id >= 0 then
        Ok(Id(id))
    else
        Error(
            { UserError.field = "id"
              error = "Invalid id. Must be a positive number" }
        )

let validateEmail email =
    let regex =
        Regex
            @"^[a-z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?(?:\.[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?)*\.[a-z]{2,20}$"

    if regex.IsMatch email then
        Ok(Email(email))
    else
        Error(
            { UserError.field = "email"
              error = "Invalid email. Must be email format in lowercase" }
        )

let createUser id email =
    match validateId id with
    | Error (idError) -> Error(idError)
    | Ok (id) -> Ok({ User.id = id; email = Email(email) })

let userCreationResult = createUser 1 "toto@user.com"

match userCreationResult with
| Ok (user) -> printfn "%A" user
| Error (errors) -> printfn "%A" errors
