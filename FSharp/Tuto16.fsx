#r @"nuget: FSharp.Data"
open FSharp.Data

[<Literal>]
let apiUrl = "https://api.stackexchange.com/2.2/"
[<Literal>]
let answerersTypeUrl = apiUrl + "tags/c/top-answerers/all_time?site=stackoverflow"

let getAnswerersUrl tag = apiUrl + "tags/" + tag + "/top-answerers/all_time?site=stackoverflow"
let getAskersUrl tag = apiUrl + "tags/" + tag + "/top-askers/all_time?site=stackoverflow"

type AnswerersType = JsonProvider<answerersTypeUrl>

let getAnswerers tag = AnswerersType.Load (getAnswerersUrl tag)

let answerers = getAnswerers "C%23"
query {
  for i in answerers.Items do
  sortByDescending i.PostCount
  take 10
  select i.User.DisplayName
}|> Seq.iter (printfn "%A")