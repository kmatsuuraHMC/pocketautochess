namespace BattleDriver

open TeamF
open BoardsF
open TurnCount

module BattleDriver =
    let BattleDriver(team1: Team, team2: Team) =
        if min team1.TeamMember.Length team2.TeamMember.Length > 0 then
            team1.BattlePerF
            team2.BattlePerF
        else
            BoardController.Turn <- TurnCount.finished
