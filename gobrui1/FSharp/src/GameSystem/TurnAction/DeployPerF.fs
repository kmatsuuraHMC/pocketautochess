namespace DeployPerF

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

type deployEffect =
    | NoChangeOfDeployTurn
    | ChangeCountToBattle
    | PositionChange of Vector2
    | PCandAddCharacter of x:float32 * y:float32 * myteam:Team* opponentTeam:Team * mtLength : int

module DeployPerF =
    let MAX_UNIT = BoardController.MAX_UNIT

    let deployPerF ((Team1: Team), (Team2: Team), previousPutPosition,mousePosition) =
        if BoardController.Deploy = DeployCount.none then
            NoChangeOfDeployTurn
        else

        if min Team1.TeamMember.Length Team2.TeamMember.Length > MAX_UNIT then
            ChangeCountToBattle
        else
            if (distBtwV2Sq previousPutPosition mousePosition <= 1.0f) then
                NoChangeOfDeployTurn
            else
                let myteam =
                    if BoardController.Deploy = DeployCount.team1
                    then Team1
                    else Team2

                let opponentTeam =
                    if BoardController.Deploy = DeployCount.team1
                    then Team2
                    else Team1

                if (myteam.TeamMember.Length > MAX_UNIT) then
                    PositionChange mousePosition
                else
                    let prop =
                        (mousePosition.x, mousePosition.y, myteam, opponentTeam,
                         myteam.TeamMember.Length)

                    PCandAddCharacter prop
