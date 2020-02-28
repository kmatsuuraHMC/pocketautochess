using UnityEngine;
using System.Collections;
using S = System;
using System.Collections.Generic;
using System.Linq;

public class Maruta : Character
{
    private void Start()
    {
        hp = 1500;
    }
    private void Update()
    {
        if (hp < 0) { Team.delete(myTeam.teamMates, this.number); }
        if (BackBoard.turn == BackBoard.Turn.battle)
        {
            if (opponentTeam.teamMates.Count == 0) { return; }
            var attackTarget = chooseAttackTarget(opponentTeam.teamMates, this);
            if (getDistanceSqToOpponent(attackTarget) < 3)
            {
                if (Random.Range(1, 10) == 1) { Explosion.Add(attackTarget); }
                SetVelocity(1, 0, 0);
                foreach (Character chara in opponentTeam.teamMates) { if (CharaUtil.charaGetDistantSq(chara, attackTarget) < 2.5) { attack(chara, 1.5f); } }
                return;
            }
            else
            {
                SetVelocity(attackTarget.tokenX - this.tokenX, attackTarget.tokenY - this.tokenY, 1.3f);
                return;
            }
        }
    }
}

