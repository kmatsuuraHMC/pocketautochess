namespace BoardsF

open DeployCount
open TurnCount
open TokenF
open PrefabCount

type BoardController() =
    inherit Token()
    static let mutable turn = TurnCount.deploy
    static let mutable deploy = DeployCount.none
    static let mutable prefab = PrefabCount.Gobrui

    static member Turn
        with get (): TurnCount = turn
        and set (v) = turn <- v

    static member Deploy
        with get (): DeployCount = deploy
        and set (v) = deploy <- v

    static member Prefab
        with get () = prefab
        and set v = prefab <- v

    static member PrefabName =
        match prefab with
        | PrefabCount.Gagoiru -> "Gagoiru"
        | PrefabCount.Gobrui -> "Gobrui"
        | PrefabCount.Maruta -> "Maruta"
        | _ -> "Gobrui"

    static member MAX_UNIT = 30

/// ターンの制御をする
and Board1(boardController) =
    inherit Token()

    member this.OnMouseDown() =
        if BoardController.Turn = TurnCount.deploy then BoardController.Deploy <- DeployCount.team1

    member this.OnMouseUp() =
        if BoardController.Turn = TurnCount.deploy then BoardController.Deploy <- DeployCount.none

    member this.OnMouseOver() =
        if BoardController.Turn = TurnCount.deploy then
            match BoardController.Deploy with
            | DeployCount.team2 -> BoardController.Deploy <- DeployCount.none
            | _ -> ()


and Board2(boardController) =
    inherit Token()

    member this.OnMouseDown() =
        if BoardController.Turn = TurnCount.deploy then BoardController.Deploy <- DeployCount.team2

    member this.OnMouseUp() =
        if BoardController.Turn = TurnCount.deploy then BoardController.Deploy <- DeployCount.none

    member this.OnMouseOver() =
        if BoardController.Turn = TurnCount.deploy then
            match BoardController.Deploy with
            | DeployCount.team1 -> BoardController.Deploy <- DeployCount.none
            | _ -> ()
