namespace Boards

open UnityEngine
open DeployCount
open TeamF
open MouseCount
open TurnCount
open DeployCount

type BoardController() =
    inherit  MonoBehaviour()
    let mutable turn = TurnCount.deploy
    let mutable deploy = DeployCount.none

    member this.Turn
        with get (): TurnCount = turn
        and set (v) = turn <- v

    member this.Deploy
        with get (): DeployCount = deploy
        and set (v) = deploy <- v

and Board1(boardController) =
    inherit MonoBehaviour()
    let team: Team = Team()
    let boardController: BoardController = boardController
    member this.Team = team

    member this.OnMouseDown =
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.team1

    member this.OnMouseUp =
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.none

    member this.OnMouseOver =
        if boardController.Turn = TurnCount.deploy then
            match boardController.Deploy with
            | DeployCount.team2 -> boardController.Deploy <- DeployCount.none
            | _ -> ()

and Board2(boardController) =
    inherit MonoBehaviour()
    let team: Team = Team()
    let boardController: BoardController = boardController
    member this.Team = team

    member this.OnMouseDown =
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.team2

    member this.OnMouseUp =
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.none

    member this.OnMouseOver =
        if boardController.Turn = TurnCount.deploy then
            match boardController.Deploy with
            | DeployCount.team1 -> boardController.Deploy <- DeployCount.none
            | _ -> ()
