namespace GagoiruF

open TeamF
open TokenF.tokenUtil
open TeamF.charaUtil
open ExplosionF
open UnityEngine
open UniRx.Async
open PrefabCount
open PrefabCountUtil
open charaUtil

type Gagoiru() =
    inherit Character()
    static let defaultHp = 3000.0f

    override this.attack attacktarget =
        attacktarget.hp <- attacktarget.hp - 5.0f
        if (Random.Range(1, 10) = 1) then
            let prefab = GetPrefab null PrefabCount.Explosion
            CreateInstance2<Explosion>(prefab, attacktarget.tokenX, attacktarget.tokenY, "Explosion") |> ignore

    override this.BattlePerF =
        if this.hp < 0.0f then
            this.myTeam.DeleteTeamMember this
            this.DestroyObj()
        let mutable i = true
        let target = this.AttackTarget
        if this.opponentTeam.TeamMember.Length <> 0 then () else i <- false
        if i then
            if (getDistanceSq (this, target) < 10.0f) then
                this.SetVelocity(1.0f, 0.0f, 0.0f)
                this.attack target
            else
                this.SetVelocity(target.tokenX - this.tokenX, target.tokenY - this.tokenY, 50.0f)

    static member Add =
        fun (x, y, my, opponent, num) ->
            addCharacter<Gagoiru> (x, y, my, opponent, PrefabCount.Gagoiru, num, "gagoirudayo", defaultHp) :> Character
