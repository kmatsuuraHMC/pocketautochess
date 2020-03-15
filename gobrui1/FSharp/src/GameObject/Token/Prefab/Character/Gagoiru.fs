namespace GagoiruF

open TeamF
open TokenF.tokenUtil
open TeamF.charaUtil
open ExplosionF
open HoutyouF
open UnityEngine
open PrefabCount
open PrefabCountUtil
open charaUtil

type Gagoiru() =
    inherit Character()
    static let defaultHp = 3000.0f
    static let defaultSpeed = 3.0f
    static let defautlRange = 6.0f
    static let defaultAttack = 20.0f

    override this.attack attacktarget =
        attacktarget.hp <- attacktarget.hp - defaultAttack
        if (Random.Range(1, 10) = 1) then Houtyou.Add this attacktarget |> ignore

    override this.BattlePerF =
        let mutable i = true
        let target = this.AttackTarget
        if this.opponentTeam.TeamMember.Length <> 0 then () else i <- false
        if i then
            if (getDistanceSq (this, target) < defautlRange * defautlRange) then
                this.SetVelocity(1.0f, 0.0f, 0.0f)
                this.attack target
            else
                this.SetVelocity(target.tokenX - this.tokenX, target.tokenY - this.tokenY, defaultSpeed)

    static member Add(pos, my, opponent, num) =
        addCharacter<Gagoiru> (pos, my, opponent, PrefabCount.Gagoiru, num, "gagoirudayo", defaultHp) :> Character
