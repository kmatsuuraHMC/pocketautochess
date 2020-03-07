namespace BoardsF

open UnityEngine
open DeployCount
open TeamF
open MouseCount
open TurnCount
open DeployCount
open TokenF
open tokenUtil
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

and Board1(boardController) =
    inherit Token()
    let mutable boardController: BoardController = boardController

    member this.BoardController
        with get () = boardController
        and set v = boardController <- v

    member this.OnMouseDownFunc =
        Debug.Log("1tokenDown")
        if BoardController.Turn = TurnCount.deploy then
            Debug.Log("1tokenDown2")
            BoardController.Deploy <- DeployCount.team1

    member this.OnMouseUpFunc =
        if BoardController.Turn = TurnCount.deploy then BoardController.Deploy <- DeployCount.none

    member this.OnMouseOverFunc =
        if BoardController.Turn = TurnCount.deploy then
            match BoardController.Deploy with
            | DeployCount.team2 -> BoardController.Deploy <- DeployCount.none
            | _ -> ()


and Board2(boardController) =
    inherit Token()
    let mutable boardController: BoardController = boardController

    member this.OnMouseDownFunc =
        Debug.Log("2tokenDown")
        if BoardController.Turn = TurnCount.deploy then
            Debug.Log("2tokenDown")
            BoardController.Deploy <- DeployCount.team2

    member this.OnMouseUpFunc =
        if BoardController.Turn = TurnCount.deploy then BoardController.Deploy <- DeployCount.none

    member this.OnMouseOverFunc =
        if BoardController.Turn = TurnCount.deploy then
            match BoardController.Deploy with
            | DeployCount.team1 -> BoardController.Deploy <- DeployCount.none
            | _ -> ()

    member this.BoardController
        with get () = boardController
        and set v = boardController <- v
