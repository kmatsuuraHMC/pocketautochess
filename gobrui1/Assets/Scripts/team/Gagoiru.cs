using UnityEngine;
using System.Collections;
using S = System;
using System.Collections.Generic;
using System.Linq;

public class Gagoiru : Character
{
    private void Start()
    {
        hp = 3000;
    }
    private void Update()
    {
        if (hp < 0) { Team.delete(myTeam.teamMates, this.number); }
        if (BackBoard.turn == BackBoard.Turn.battle)
        {
            if (opponentTeam.teamMates.Count == 0) { return; }
            var attackTarget = chooseAttackTarget(opponentTeam.teamMates, this);
            if (getDistanceSqToOpponent(attackTarget) < 10)
            {
                SetVelocity(1, 0, 0);
                attack(attackTarget, 10);
                return;
            }
            else
            {
                SetVelocity(attackTarget.tokenX - this.tokenX, attackTarget.tokenY - this.tokenY, 1);
                return;
            }
        }
    }
    public override void attack(Character character, float attackPoint)
    {
        if (Random.Range(1, 10) == 1) { Houtyou.Add(this, character); }
        var hpdayo = character.hp;
        character.hp = hpdayo - attackPoint;
    }

    public override Character chooseAttackTarget(List<Character> teamMates, Character chara)
    {
        var xyn_c = new gagoiruComparer(chara);
        var teamMates_xyn = new List<Character>(teamMates);
        teamMates_xyn.Sort(xyn_c);
        var chosen = teamMates_xyn[0];
        return chosen;
    }

    public class gagoiruComparer : IComparer<Character>
    {
        public gagoiruComparer(Character chara)
        {
            thisCCharacter = chara;
        }
        private Character thisCCharacter;
        public int Compare(Character v1, Character v2)
        {
            var distance1 = charaGetDistantSq(thisCCharacter, v1);
            var distance2 = charaGetDistantSq(thisCCharacter, v2);
            if (distance1 < distance2) { return -1; }
            return 1;
        }
    }
}
