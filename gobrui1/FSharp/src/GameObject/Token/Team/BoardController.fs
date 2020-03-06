namespace BoardsF

open UnityEngine
open DeployCount
open TeamF
open MouseCount
open TurnCount
open DeployCount
open TokenF
open tokenUtil

type BoardController() =
    inherit Token()
    let mutable turn = TurnCount.deploy
    let mutable deploy = DeployCount.none

    member this.Turn
        with get (): TurnCount = turn
        and set (v) = turn <- v

    member this.Deploy
        with get (): DeployCount = deploy
        and set (v) = deploy <- v

and Board1(boardController) =
    inherit Token()
    let mutable team: Team = Team()
    let mutable boardController: BoardController = boardController

    member this.Team
        with get () = team
        and set v = team <- v

    member this.BoardController
        with get () = boardController
        and set v = boardController <- v

    member this.OnMouseDownFunc =
        Debug.Log
            (if this.BoardController.Turn = TurnCount.deploy
             then "deploy"
             else "other")
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.team1

    member this.OnMouseUpFunc =
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.none

    member this.OnMouseOverFunc =
        if boardController.Turn = TurnCount.deploy then
            match boardController.Deploy with
            | DeployCount.team2 -> boardController.Deploy <- DeployCount.none
            | _ -> ()


and Board2(boardController) =
    inherit Token()
    let mutable team: Team = Team()
    let mutable boardController: BoardController = boardController

    member this.Team
        with get () = team
        and set v = team <- v

    member this.OnMouseDownFunc =
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.team2

    member this.OnMouseUpFunc =
        if boardController.Turn = TurnCount.deploy then boardController.Deploy <- DeployCount.none

    member this.OnMouseOverFunc =
        if boardController.Turn = TurnCount.deploy then
            match boardController.Deploy with
            | DeployCount.team1 -> boardController.Deploy <- DeployCount.none
            | _ -> ()

    member this.BoardController
        with get () = boardController
        and set v = boardController <- v

module BoardUtil =
    let addBoard1 (boardController: BoardController) =
        Debug.Log("fuga")
        let prefab = box <| GetPrefab(null, "Board1") :?> Board1
        Debug.Log("hoge")
        prefab.BoardController <- boardController
        prefab.Team <- Team()
        prefab

    let addBoard2 (boardController: BoardController) =
        let prefab = box <| GetPrefab(null, "Board2") :?> Board2
        prefab.BoardController <- boardController
        prefab.Team <- Team()
        prefab

    let addBoardController =
        Debug.Log("piyo")
        let board = (box <| GetPrefab(null, "BoardController")) :?> BoardController
        Debug.Log("piyopiiyo")
        board.Turn <- TurnCount.deploy
        board.Deploy <- DeployCount.none
        board
