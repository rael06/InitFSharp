#r @"nuget: FSharp.Data"
open FSharp.Data

[<Literal>]
let apiUrl = "https://api.stackexchange.com/2.2/"

[<Literal>]
let answerersTypeUrl =
    apiUrl
    + "tags/c/top-answerers/all_time?site=stackoverflow"

[<Literal>]
let askersTypeUrl =
    apiUrl
    + "tags/c/top-askers/all_time?site=stackoverflow"

type AnswerersType = JsonProvider<answerersTypeUrl>
type AskersType = JsonProvider<askersTypeUrl>

let getAnswerersUrl tag =
    apiUrl
    + "tags/"
    + tag
    + "/top-answerers/all_time?site=stackoverflow"

let getAskersUrl tag =
    apiUrl
    + "tags/"
    + tag
    + "/top-askers/all_time?site=stackoverflow"

let getAnswerers tag =
    let data = AnswerersType.Load(getAnswerersUrl tag)

    query {
        for i in data.Items do
            sortByDescending i.PostCount
            take 10
            select i.User.DisplayName
    }

let getAskers tag =
    let data = AskersType.Load(getAskersUrl tag)

    query {
        for i in data.Items do
            sortByDescending i.PostCount
            take 10
            select i.User.DisplayName
    }

type Target =
    | Answerers
    | Askers

type Message =
    | AskTagInfoMessage of Target * string * AsyncReplyChannel<seq<string>>
    | AskTopTagsMessage of string * AsyncReplyChannel<obj>

let agent =
    MailboxProcessor<Message>.Start
        (fun inbox ->
            let rec loop () =
                async {
                    let! msg = inbox.Receive()

                    match msg with
                    | AskTagInfoMessage (target, tag, channel) ->
                        match target with
                        | Answerers -> channel.Reply(getAnswerers tag)
                        | Askers -> channel.Reply(getAskers tag)
                    | AskTopTagsMessage (tag, channel) -> printfn "%s" tag

                    return! loop ()
                }

            loop ())

agent.PostAndReply(fun channel -> AskTagInfoMessage(Answerers, "C%23", channel))
|> Seq.iter (printfn "%s")
