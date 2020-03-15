namespace TeamF

open UnityEngine
open FSharp.Core
open TokenF
open tokenUtil
open PrefabCount

exception TeamMemberNotFoundException of string

[<AbstractClass>]
type Character() =
    inherit Token()
    let mutable _myTeam, _opponentTeam, _charaNum, _hp, _race = Team(), Team(), 0, 0.0f, PrefabCount.Gobrui

    member this.myTeam
        with get () = _myTeam
        and set v = _myTeam <- v

    member this.opponentTeam
        with get () = _opponentTeam
        and set v = _opponentTeam <- v

    member this.number
        with get () = _charaNum
        and set v = _charaNum <- v

    member this.hp
        with get () = _hp
        and set hp = _hp <- hp

    member this.race
        with get (): PrefabCount = _race
        and set v = _race <- v

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

    member this.DeleteTeamMember character: Unit =
        if Array.contains character teamMember then
            teamMember <- Array.except [| character |] teamMember
            character.DestroyObj()
        else
            raise (TeamMemberNotFoundException <| this.ToString() + "does not have Member " + character.name)

    //         for (int i = 0; i < 5; i++)
    //             Particle.Add(searchedCharaByName.tokenX, searchedCharaByName.tokenY);
    member this.SearchedCharaByNumber(num) =
        Array.findBack (fun (chara: Character) ->
            let number = (chara.number: int) in number = num) teamMember

    member this.BattlePerF =
        for i in teamMember do
            if i.hp < 0.0f then
                this.DeleteTeamMember i
                i.DestroyObj()
            i.BattlePerF

module charaUtil =
    let addCharacter<'T when 'T :> Character> (pos, _myTeam, _opponentTeam, _race, _charaNum, _name, _hp) =

        let prefab = GetPrefab null _race
        let hoge = CreateInstance2<'T>(prefab, pos, _name)
        hoge.hp <- _hp
        hoge.myTeam <- _myTeam
        hoge.opponentTeam <- _opponentTeam
        hoge.number <- _charaNum
        hoge.race <- _race
        hoge
