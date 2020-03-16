namespace GlobalStateF
// ココらへん、制御構造を書き直す必要がある
open UnityEngine
open TeamF
open TurnCount
open BoardsF
open Vector3Util
open OperatorV3
open PreActionDriver.PreActionDriver
open DeployPerF
open DeployPerF.DeployPerF
open DeployDriver.DeployDriver
open BattleDriver.BattleDriver
open UDPManager

type GlobalState() =
    inherit MonoBehaviour()
    let mutable prePosition = Vector2(0.0f, 0.0f)
    let team1 = Team()
    let team2 = Team()

    interface GlobalStateI.GlobalStateI with

        member this.previousPutPosition
            with get () = prePosition
            and set v = prePosition <- v

        member this.Team1 = Team()
        member this.Team2 = Team()

    member this.Update() =
        PreActionDriver()
        match BoardController.Turn with
        | TurnCount.deploy ->
            let mousePosition = toV2 <| Camera.main.ScreenToWorldPoint Input.mousePosition
            DeployDriver(this, deployPerF (team1, team2, prePosition, mousePosition))
        | TurnCount.battle -> BattleDriver(team1, team2)
        | _ -> ()
