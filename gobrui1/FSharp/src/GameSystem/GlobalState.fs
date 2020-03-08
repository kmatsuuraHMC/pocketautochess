namespace GlobalStateF
// ココらへん、制御構造を書き直す必要がある
open UnityEngine
open TeamF
open TurnCount
open DeployCount
open BoardsF
open Vector3Util
open OperatorV2
open OperatorV3
open GagoiruF
open GobruiF
open MarutaF
open PrefabCount


type GlobalState() =
    inherit MonoBehaviour()
    let mutable point1, point2 = Vector2(0.0f, 0.0f), Vector2(0.0f, 0.0f)
    let mutable Team1 = Team()
    let mutable Team2 = Team()
    let MAX_UNIT = 30

    member this.Start = ()

    member this.UpdateFunc =
        if (Input.GetKey(KeyCode.A)) then BoardController.Prefab <- PrefabCount.Gobrui
        if (Input.GetKey(KeyCode.S)) then BoardController.Prefab <- PrefabCount.Maruta
        if (Input.GetKey(KeyCode.D)) then BoardController.Prefab <- PrefabCount.Gagoiru
        match BoardController.Turn with
        | TurnCount.deploy ->
            if min Team1.TeamMember.Length Team2.TeamMember.Length > MAX_UNIT then
                BoardController.Turn <- TurnCount.battle
            point2 <- toV2 <| Camera.main.ScreenToWorldPoint Input.mousePosition
            if (distBtwV2Sq point1 point2 > 1.0f) then
                point1 <- point2
                let whichNumber =
                    match BoardController.Deploy with
                    | DeployCount.team1 -> Team1.TeamMember.Length
                    | DeployCount.team2 -> Team2.TeamMember.Length
                    | _ -> MAX_UNIT + 100 // 決してマッチしない
                if BoardController.Deploy <> DeployCount.none then
                    if (whichNumber <= MAX_UNIT) then
                        let myteam =
                            if BoardController.Deploy = DeployCount.team1
                            then Team1
                            else Team2

                        let opponentTeam =
                            if BoardController.Deploy = DeployCount.team1
                            then Team2
                            else Team1

                        let addingCharacter =
                            let prop = (point1.x, point1.y, myteam, opponentTeam, myteam.TeamMember.Length)
                            match BoardController.Prefab with
                            | PrefabCount.Gobrui -> Gobrui.Add prop
                            | PrefabCount.Gagoiru -> Gagoiru.Add prop
                            | PrefabCount.Maruta -> Maruta.Add prop
                            | _ -> Gobrui.Add prop

                        myteam.Add addingCharacter
        | TurnCount.battle ->
            if min Team1.TeamMember.Length Team2.TeamMember.Length > 0 then
                Team1.BattlePerF
                Team2.BattlePerF
            else
                BoardController.Turn <- TurnCount.finished
        | _ -> ()
