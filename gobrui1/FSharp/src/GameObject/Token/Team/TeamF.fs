namespace TeamF

open UnityEngine
open FSharp.Core
open TokenF
open tokenUtil

[<AbstractClass>]
type Character() =
    inherit Token()
    let mutable _myTeam, _opponentTeam, _charaNum, _hp, _race = Team(), Team(), 0, 0.0f, ""

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
        with get () = _race
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
    member this.DeleteTeamMember character: Unit = teamMember <- Array.except [| character |] teamMember
    //         for (int i = 0; i < 5; i++)
    //             Particle.Add(searchedCharaByName.tokenX, searchedCharaByName.tokenY);
    member this.SearchedCharaByNumber(num) =
        Array.findBack (fun (chara: Character) ->
            let number = (chara.number: int) in number = num) teamMember

    member this.BattlePerF =
        for i in teamMember do
            i.BattlePerF

module charaUtil =
    let addCharacter (chartX, chartY, _myTeam, _opponentTeam, _race, _hp, _charaNum): Character =
        let prefab = box (GetPrefab(null, _race)) :?> Character
        prefab.tokenX <- chartX
        prefab.tokenY <- chartY
        prefab.hp <- _hp
        prefab.myTeam <- _myTeam
        prefab.opponentTeam <- _opponentTeam
        prefab.number <- _charaNum
        prefab.race <- _race
        prefab
