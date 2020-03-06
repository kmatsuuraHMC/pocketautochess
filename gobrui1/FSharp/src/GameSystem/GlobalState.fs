namespace GlobalStateF
// ココらへん、制御構造を書き直す必要がある
open UnityEngine
open TeamF
open TokenF
open TurnCount
open DeployCount
open BoardsF
open Vector3Util
open OperatorV2
open OperatorV3
open GagoiruF
open BoardUtil
open charaUtil
open System
open Microsoft.FSharp.Linq
open tokenUtil


type GlobalState() =
    inherit MonoBehaviour()
    let mutable number1, number2 = 0, 0
    let mutable point1, point2 = Vector2(0.0f, 0.0f), Vector2(0.0f, 0.0f)
    let mutable boardControllerBox = None
    let mutable board1Box = None
    let mutable board2Box = None
    let MAX_UNIT = 30
    let race = "Gagoiru"

    member this.Start =
        let boardController = addBoardController
        boardControllerBox <- Some boardController
        board1Box <- Some <| addBoard1 boardController
        board2Box <- Some <| addBoard2 boardController

    member this.Update =
        Debug.Log("hogehoge")
        match (boardControllerBox, board1Box, board2Box) with
        | (Some boardController, Some board1, Some board2) ->
            match boardController.Turn with
            | TurnCount.deploy ->
                Debug.Log("hoge")
                point2 <- toV2  <| Camera.main.ScreenToWorldPoint Input.mousePosition
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

                        myteam.Add
                            (addCharacter
                                (point2.x, point2.y, myteam, opponentTeam, "Gagoiru", 5.0f, myteam.TeamMember.Length))

            | TurnCount.battle ->
                if (board1.Team.TeamMember.Length + board2.Team.TeamMember.Length > 0) then
                    board1.Team.BattlePerF
                    board2.Team.BattlePerF
                else
                    boardController.Turn <- TurnCount.finished
            | _ -> ()
        | _ -> ()
