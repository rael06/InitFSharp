open System
open System.Globalization
open Microsoft.FSharp.Core

let data =
    [ "[20/11/2017 08:00:00] [->] [Jose 0678256374] [00:08:34] [2.3]"
      "[20/11/2017 18:05:20] [<-] [Audrey 0614237645] [00:01:40]"
      "[21/11/2017 06:12:54] [->] [Audrey 0614237645] [00:03:12] [1.2]"
      //      "ERROR"
      "[21/11/2017 06:12:54] [->] [Audrey 0614237645] [sldkdjfhaslkjdfh] [1.2]"
      "[22/11/2017 23:32:43] [->] [Martin 0696752853] [01:02:54] [8.1]" ]


type User = { name: string; phone: string }

type Outgoing =
    { dateTime: DateTime
      recipient: User
      duration: TimeSpan
      price: float }

type Incoming =
    { dateTime: DateTime
      source: User
      duration: TimeSpan }

type PhoneMessage =
    | Outgoing of Outgoing
    | Incoming of Incoming


let (|Date|_|) (str: string) =
    let success, dateTime = DateTime.TryParse str
    if success then Some(dateTime) else None

let (|Duration|_|) (str: string) =
    let success, timeSpan = TimeSpan.TryParse str
    if success then Some(timeSpan) else None

let (|Price|_|) (str: string) =
    let success, price = Double.TryParse str
    if success then Some(price) else None

let (|UserInfo|_|) (str: string) =
    printfn "Str: %s" str
    let info = str.Split " "
    printfn "Info: %A" info
    if info.Length = 2 then Some({ User.name = info[0]; phone = info[1] }) else None

let createOutgoing (date, recipient, duration, price) =
    { Outgoing.dateTime = date
      recipient = recipient
      duration = duration
      price = price }

let createIncoming (date, source, duration) =
    { Incoming.dateTime = date
      source = source
      duration = duration }

let parseLine (line: string) : option<PhoneMessage> =
    let parts =
        line.Split([| '['; ']' |])
        |> Array.filter (String.IsNullOrWhiteSpace >> not)

    match parts with
    | [| Date date; "->"; UserInfo user; Duration duration; Price price |] -> Some(PhoneMessage.Outgoing(createOutgoing (date, user, duration, price)))
    | [| Date date; "<-"; UserInfo user; Duration duration |] -> Some(PhoneMessage.Incoming(createIncoming (date, user, duration)))
    | _ -> None

data |> List.map parseLine |> printfn "%A"
