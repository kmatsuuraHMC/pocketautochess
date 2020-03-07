namespace GobruiF

open TeamF
open TokenF.tokenUtil
open TeamF.charaUtil
open ExplosionF
open UnityEngine
open UniRx.Async
open PrefabCount
open charaUtil

type Gobrui() =
    inherit Character()
    static let defaultHp = 5000.0f

    override this.attack attacktarget =
        attacktarget.hp <- attacktarget.hp - 5.0f
        if (Random.Range(1, 1000) = 1) then
            let prefab = GetPrefab null PrefabCount.Explosion
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

    static member Add: float32 * float32 * Team * Team * int -> Character =
        fun (x, y, my, opponent, num) ->
            addCharacter<Gobrui> (x, y, my, opponent, PrefabCount.Gobrui, num, "Gobruidayo", defaultHp) :> Character
