namespace TeamF

open UnityEngine
open FSharp.Core
open TokenF
open tokenUtil

[<AbstractClass>]
type Character(_prefab, _myTeam, _opponentTeam, _charaNum, _hp) =
    inherit Token()
    let mutable _hp = _hp
    member this.myTeam: Team = _myTeam
    member this.opponentTeam: Team = _opponentTeam
    member this.number: int = _charaNum

    member this.hp
        with get () = _hp
        and set hp = _hp <- hp

    abstract attack: Character -> Unit
    abstract BattlePerF: Unit
    abstract AttackTarget: Character
    default this.AttackTarget =
        Array.minBy (fun (chara: Character) -> getDistanceSq (this, chara)) (this.opponentTeam.TeamMember)

    member this.attacked (attackedPoint: float32) = _hp <- _hp - attackedPoint
    member this.ToCharacter(): Character = this


and Team() =
    let mutable teamMember = Array.empty
    member this.TeamMember: array<Character> = teamMember
    member this.Add character = teamMember <- Array.append this.TeamMember [| character |]
    member this.DeleteTeamMember character: Unit = teamMember <- Array.except [| character |] teamMember
    //         for (int i = 0; i < 5; i++)
    //             Particle.Add(searchedCharaByName.tokenX, searchedCharaByName.tokenY);
    member this.SearchedCharaByNumber(num) =
        Array.findBack (fun (chara: Character) ->
            let number = (chara.number: int) in number = num) teamMember

    member this.BattlePerF =
        for i in teamMember do
            i.BattlePerF
