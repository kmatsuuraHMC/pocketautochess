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
    member this.number: nativeint = _charaNum
    member this.hp
        with get () = _hp
        and set hp = _hp <- hp

    abstract AttackTarget: Character
    abstract attack: Character -> Unit
    abstract ActionPerF: Unit

    member this.attacked (attackedPoint: float32) = _hp <- _hp - attackedPoint
    member this.ToCharacter(): Character = this


and Team() =
    let mutable teamMember = Array.empty
    member this.TeamMember: array<Character> = teamMember
    member this.Add character = teamMember <- Array.append this.TeamMember [| character |]
    member this.DeleteTeamMember character: Unit = teamMember <- Array.except ([| character |]) teamMember
    //         for (int i = 0; i < 5; i++)
    //             Particle.Add(searchedCharaByName.tokenX, searchedCharaByName.tokenY);
    member this.SearchedCharaByNumber(num: nativeint) =
        Array.findBack (fun (chara: Character) ->
            let number = (chara.number: nativeint) in number = num) teamMember