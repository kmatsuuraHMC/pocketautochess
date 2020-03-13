namespace DeployDriver

open TeamF
open DeployCount
open DeployPerF
open BoardsF
open TurnCount
open GlobalStateI
open GobruiF
open GagoiruF
open MarutaF
open UnityEngine

module DeployDriver =
    let DeployDriver((state: GlobalStateI), a: deployEffect) =
        match a with
        | NoChangeOfDeployTurn -> ()
        | ChangeCountToBattle -> BoardController.Turn <- TurnCount.battle
        | PositionChange b -> state.previousPutPosition <- b
        | PCandAddCharacter(mousePosition, myteam, opponentTeam, leng) ->
            let prop = (mousePosition, myteam, opponentTeam, leng)

            let addingCharacter =
                match BoardController.Prefab with
                | PrefabCount.Gobrui -> Gobrui.Add prop
                | PrefabCount.Gagoiru -> Gagoiru.Add prop
                | PrefabCount.Maruta -> Maruta.Add prop
                | _ -> Gobrui.Add prop
            state.previousPutPosition <- mousePosition
            myteam.Add addingCharacter
