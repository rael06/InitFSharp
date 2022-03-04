// #!/usr/bin/env -S dotnet fsi

open System

printfn "Bind, Map and Apply"

let bind = Result.bind
// if x is ok then f(x) else keep error
let (>>=) x f = bind f x

// if x is ok then ok(f x) else keep error
let map = Result.map
let (<!>) = map

let apply f x =
    match f, x with
    | Ok f, Ok x -> Ok(f x)
    | Error e, Ok _ -> Error e
    | Ok _, Error e -> Error e
    | Error e1, Error e2 -> Error(e1 @ e2)

let (<*>) = apply

type ResultBuilder() =
    member this.Return x = Ok x
    member this.Bind(x, f) = Result.bind f x

let result = new ResultBuilder()

module Domain =
    type UserError =
        | UserIdInvalid
        | UserEmailIsEmpty
        | UserEmailInvalid

    type UserId = private UserId of int

    module UserId =
        let create id =
            if id <= 0 then
                Error [ UserIdInvalid ]
            else
                Ok(UserId id)

    type UserEmail = private UserEmail of string

    module UserEmail =
        let create email =
            if String.IsNullOrWhiteSpace email then
                Error [ UserEmailIsEmpty ]
            elif email.Contains("@") |> not then
                Error [ UserEmailInvalid ]
            else
                Ok(UserEmail email)


    type User = { Id: UserId; Email: UserEmail }

module UserService =
    open Domain
    let private createUser id email = { Id = id; Email = email }

    // applicative style (get all errors)
    let createUserV1 id email =
        let id = UserId.create id
        let email = UserEmail.create email
        createUser <!> id <*> email

    // monadic style (if error then break)
    let createUserV2 id email =
        UserId.create id
        >>= (fun id ->
            UserEmail.create email
            >>= (fun email -> Ok(createUser id email)))

    //  monadic style with computation expression
    let createUserV3 id email =
        result {
            let! id = UserId.create id
            let! email = UserEmail.create email
            return (createUser id email)
        }


UserService.createUserV1 -1 "hello"
|> printfn "%A"

UserService.createUserV2 -1 "hello"
|> printfn "%A"

UserService.createUserV3 -1 "hello"
|> printfn "%A"
