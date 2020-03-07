namespace GagoiruF

open TeamF
open TokenF.tokenUtil
open TeamF.charaUtil
open ExplosionF
open UnityEngine

type Gagoiru() =
    inherit Character()

    override this.attack attacktarget =
        attacktarget.hp <- attacktarget.hp - 5.0f
        if (Random.Range(1, 1000) = 1) then
            let prefab = GetPrefab null "Explosion"
            CreateInstance2<Explosion>(prefab, attacktarget.tokenX, attacktarget.tokenY, "Explosion") |> ignore

    override this.BattlePerF =
        if this.hp < 0.0f then
            this.myTeam.DeleteTeamMember this
            this.DestroyObj()
        let mutable i = true
        if this.opponentTeam.TeamMember.Length <> 0 then () else i <- false
        let target = this.AttackTarget
        if i then
            if (getDistanceSq (this, target) < 10.0f) then
                this.SetVelocity(1.0f, 0.0f, 0.0f)
                this.attack target
            else
                this.SetVelocity(target.tokenX - this.tokenX, target.tokenY - this.tokenY, 50.0f)
