namespace MarutaF

open TeamF
open TokenF.tokenUtil
open TeamF.charaUtil
open ExplosionF
open UnityEngine
open PrefabCount
open charaUtil

type Maruta() =
    inherit Character()
    static let defaultHp = 1500.0f
    static let defaultSpeed = 4.0f
    static let defaultAttack = 10.0f
    static let defaultRange = 3.0f
    static let explosionRange = 1.0f

    override this.attack attacktarget =
        if (Random.Range(1, 10) = 1) then
            let prefab = GetPrefab null PrefabCount.Explosion
            CreateInstance2<Explosion>(prefab, attacktarget.pos, "Explosion") |> ignore
        for other in this.opponentTeam.TeamMember do
            if (getDistanceSq (attacktarget, other) < explosionRange * explosionRange) then
                other.hp <- other.hp - defaultAttack


    override this.BattlePerF =
        let mutable i = true
        if this.opponentTeam.TeamMember.Length <> 0 then () else i <- false
        let target = this.AttackTarget
        if i then
            if (getDistanceSq (this, target) < defaultRange * defaultRange) then
                this.SetVelocity(1.0f, 0.0f, 0.0f)
                this.attack target
            else
                this.SetVelocity(target.tokenX - this.tokenX, target.tokenY - this.tokenY, defaultSpeed)

    static member Add(pos, my, opponent, num) =
        addCharacter<Maruta> (pos, my, opponent, PrefabCount.Maruta, num, "Marutadayo", defaultHp) :> Character
