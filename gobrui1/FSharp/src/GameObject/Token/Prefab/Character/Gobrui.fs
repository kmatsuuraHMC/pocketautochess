namespace GobruiF

open TeamF
open TokenF.tokenUtil
open TeamF.charaUtil
open ExplosionF
open UnityEngine
open PrefabCount
open charaUtil

type Gobrui() =
    inherit Character()
    static let defaultHp = 9000.0f
    static let defaultSpeed = 10.0f
    static let defaultRange = 1.5f
    static let defaultAttack = 13.0f

    override this.attack attacktarget =
        attacktarget.hp <- attacktarget.hp - defaultAttack
        if (Random.Range(1, 10) = 1) then
            let prefab = GetPrefab null PrefabCount.Cheese
            CreateInstance2<CheeseF.Cheese>(prefab, attacktarget.pos, "Cheese") |> ignore

    override this.BattlePerF =
        let target = this.AttackTarget
        if this.opponentTeam.TeamMember.Length <> 0 then
            if (getDistanceSq (this, target) < defaultRange * defaultRange) then
                this.SetVelocity(1.0f, 0.0f, 0.0f)
                this.attack target
            else
                this.SetVelocity(target.tokenX - this.tokenX, target.tokenY - this.tokenY, defaultSpeed)

    static member Add(pos, myTeam, opponentTeam, charaNum) =
        addCharacter<Gobrui> (pos, myTeam, opponentTeam, PrefabCount.Gobrui, charaNum, "Gobruidayo", defaultHp) :> Character
