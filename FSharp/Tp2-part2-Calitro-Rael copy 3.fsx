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
module UserModule =

    open System.Text.RegularExpressions

    type UserError =
        { field: string
          value: obj
          error: string }

    type Id = Id of int
    type Email = Email of string

    type UserField =
        | Id of int
        | Email of string

    type User = { id: UserField; email: UserField }

    type FieldValidationResult =
        { value: Option<UserField>
          error: Option<UserError> }

    let validateId id =
        if id >= 0 then
            { FieldValidationResult.value = Some(Id(id))
              error = None }
        else
            { FieldValidationResult.value = None
              error =
                Some(
                    { UserError.field = "Id"
                      value = id
                      error = "Id is invalid. Must be a positive integer" }
                ) }

    let validateEmail email =
        let regex =
            Regex
                @"^[a-z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?(?:\.[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?)*\.[a-z]{2,20}$"

        if (regex.IsMatch email) then
            { FieldValidationResult.value = Some(Email(email))
              error = None }
        else
            { FieldValidationResult.value = None
              error =
                Some(
                    { UserError.field = "Email"
                      value = email
                      error = "Email is invalid. Must be email format and lowercase" }
                ) }

    let createUser id email =
        printfn "User creation trial with id: '%d', email: '%s'" id email
        let idField = validateId id
        let emailField = validateEmail email

        let fieldValidationResults = [ idField; emailField ]

        let errors =
            fieldValidationResults
            |> List.filter (fun x -> x.value.IsNone)

        if (errors.Length > 0) then
            Error(errors |> List.map (fun x -> x.error.Value))
        else
            Ok(
                { User.id = idField.value.Value
                  email = emailField.value.Value }
            )


    let showResult (userCreationResult: Result<User, list<UserError>>) =
        match userCreationResult with
        | Ok (user) -> printfn "%A" user
        | Error (errors) -> errors |> Seq.iter (printfn "%A")

        printfn "--------------------"

    createUser 1 "toto@user.com" |> showResult
    createUser -1 "toto" |> showResult
    createUser -1 "toto@user.com" |> showResult
    createUser 1 "toto" |> showResult
