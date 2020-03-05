namespace BackBoard
// ココらへん、制御構造を書き直す必要がある
open UnityEngine
open TeamF
open TokenF
open TurnCount
open DeployCount
open Boards
open Vector3Util
open OperatorV2
open OperatorV3
open GagoiruF

type GlobalState() =
    inherit MonoBehaviour()
    let mutable number1, number2 = 0, 0
    let mutable point1, point2 = Vector2(0.0f, 0.0f), Vector2(0.0f, 0.0f)
    let boardController = BoardController()
    let board1, board2 = Board1(boardController), Board2(boardController)
    let MAX_UNIT = 30
    let race = "Gagoiru"

    member this.Update =
        match boardController.Turn with
        | TurnCount.deploy ->
            point2 <- toV2 (Camera.main.ScreenToWorldPoint(Input.mousePosition))
            if (distBtwV2Sq point1 point2 > 1.0f) then
                point1 <- point2
                let isStatusTeam1 = boardController.Deploy = DeployCount.team1

                let whichNumber =
                    if isStatusTeam1 then board1.Team.TeamMember.Length else board2.Team.TeamMember.Length
                if (whichNumber <= MAX_UNIT) then
                    let myteam =
                        if isStatusTeam1 then board1.Team else board2.Team

                    let opponentTeam =
                        if isStatusTeam1 then board2.Team else board1.Team

                    myteam.Add(Gagoiru(null, myteam, opponentTeam, whichNumber))

        | TurnCount.battle ->
            if (board1.Team.TeamMember.Length + board2.Team.TeamMember.Length > 0) then
                board1.Team.BattlePerF
                board2.Team.BattlePerF
            else
                boardController.Turn <- TurnCount.finished
        | _ -> ()
