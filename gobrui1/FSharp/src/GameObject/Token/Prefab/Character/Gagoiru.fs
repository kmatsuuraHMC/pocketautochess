namespace GagoiruF

open TeamF
open TokenF.tokenUtil

type Gagoiru(_gameObject, _myTeam, _opponentTeam, _charaNum) =
    inherit Character(_gameObject, _myTeam, _opponentTeam, _charaNum, 3000.0f)

    override this.attack attacktarget = attacktarget.hp <- attacktarget.hp - 5.0f
    override this.BattlePerF =
        if this.hp < 0.0f then this.myTeam.DeleteTeamMember this
        let mutable i = true
        if this.opponentTeam.TeamMember.Length <> 0 then () else i <- false
        let target = this.AttackTarget
        if i then
            if (getDistanceSq (this, target) < 10.0f) then
                this.SetVelocity(1.0f, 0.0f, 0.0f)
                this.attack target
            else
                this.SetVelocity(target.tokenX - this.tokenX, target.tokenY - this.tokenY, 1.0f)
