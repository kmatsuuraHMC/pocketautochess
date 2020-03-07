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
open charaUtil
open System
open Microsoft.FSharp.Linq
open tokenUtil


type GlobalState() =
    inherit MonoBehaviour()
    let mutable number1, number2 = 0, 0
    let mutable point1, point2 = Vector2(0.0f, 0.0f), Vector2(0.0f, 0.0f)
    let mutable boardControllerBox = None
    let mutable Team1 = Team()
    let mutable Team2 = Team()
    let MAX_UNIT = 30
    let race = "Gagoiru"

    member this.Start = ()

    member this.UpdateFunc =
        match BoardController.Turn with
        | TurnCount.deploy ->
            point2 <- toV2 <| Camera.main.ScreenToWorldPoint Input.mousePosition
            if (distBtwV2Sq point1 point2 > 1.0f) then
                point1 <- point2
                Debug.Log("Turn:" + BoardController.Deploy.ToString("d"))
                let whichNumber =
                    match BoardController.Deploy with
                    | DeployCount.team1 -> Team1.TeamMember.Length
                    | DeployCount.team2 -> Team2.TeamMember.Length
                    | _ -> MAX_UNIT + 100 // 以下を発火させない

                let mutable continue2 = true
                if BoardController.Deploy = DeployCount.none then continue2 <- false
                if continue2 then
                    if (whichNumber <= MAX_UNIT) then
                        let mutable continue1 = true
                        if BoardController.Deploy = DeployCount.none then continue1 <- false
                        if continue1 then
                            let myteam =
                                if BoardController.Deploy = DeployCount.team1
                                then Team1
                                else Team2

                            let opponentTeam =
                                if BoardController.Deploy = DeployCount.team1
                                then Team2
                                else Team1

                            let addingCharacter =
                                (addCharacter<Gagoiru>
                                    (point2.x, point2.y, myteam, opponentTeam, "Gagoiru", 3000.0f,
                                     myteam.TeamMember.Length)) :> Character
                            myteam.Add(addingCharacter)
                            if min Team1.TeamMember.Length Team2.TeamMember.Length > 30 then
                                BoardController.Turn <- TurnCount.battle
        | TurnCount.battle ->
            if (Team1.TeamMember.Length + Team2.TeamMember.Length > 0) then
                Team1.BattlePerF
                Team2.BattlePerF
            else
                BoardController.Turn <- TurnCount.finished
        | _ -> ()
