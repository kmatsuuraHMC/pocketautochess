namespace GagoiruF

open TeamF
open TokenF.tokenUtil

type Gagoiru(_gameObject, _myTeam, _opponentTeam, _charaNum) =
    inherit Character(_gameObject, _myTeam, _opponentTeam, _charaNum, 3000.0f)
    override this.AttackTarget =
        Array.minBy (fun chara -> getDistanceSq (this, chara) ) this.opponentTeam.TeamMember

    override this.attack attacktarget =
        attacktarget.hp <- attacktarget.hp - 5.0f
    override this.ActionPerF =
        if this.hp < 0.0f then this.myTeam.DeleteTeamMember this else ()

//             if (BackBoard.turn == BackBoard.Turn.battle)
//             {
//                 if (opponentTeam.TeamMember.Count() == 0) { return; }
//                 var attackTarget = chooseAttackTarget(opponentTeam.TeamMember, this);
//                 if (getDistanceSqToOpponent(attackTarget) < 10)
//                 {
//                     SetVelocity(1, 0, 0);
//                     attack(attackTarget, 10);
//                     return;
//                 }
//                 else
//                 {
//                     SetVelocity(attackTarget.tokenX - this.tokenX, attackTarget.tokenY - this.tokenY, 1);
//                     return;
//                 }
//             }
//         }
//         public override Character AttackTarget
//         {
//             get() = { return opponentTeam.Min()
//     }
// }
// public override void attack(Character character)
// {
//     if (Random.Range(1, 10) == 1) { Houtyou.Add(this, character); }
//     var hpdayo = character.hp;
//     character.hp = hpdayo - 5;
// }