using UnityEngine;
using System.Collections;
using S = System;
using System.Collections.Generic;
using System.Linq;
using TeamF;

[RequireComponent(typeof(SpriteRenderer))]
public class Gagoiru : GagoiruF.Gagoiru
{  
    public Gagoiru() : base() { }
    //     public Gagoiru(GameObject _gameObject, Team _myTeam, Team _opponentTeam, S.IntPtr _charaNum, float _hp) : base(_gameObject, _myTeam, _opponentTeam, _charaNum, _hp) { }
    //     private void Start()
    //     {
    //         hp = 3000;
    //     }
    //     private void Update()
    //     {
    //         if (hp < 0) { myTeam.DeleteTeamMember(this); }
    //         if (BackBoard.turn == BackBoard.Turn.battle)
    //         {
    //             if (opponentTeam.TeamMember.Count() == 0) { return; }
    //             var attackTarget = chooseAttackTarget(opponentTeam.TeamMember, this);
    //             if (getDistanceSqToOpponent(attackTarget) < 10)
    //             {
    //                 SetVelocity(1, 0, 0);
    //                 attack(attackTarget, 10);
    //                 return;
    //             }
    //             else
    //             {
    //                 SetVelocity(attackTarget.tokenX - this.tokenX, attackTarget.tokenY - this.tokenY, 1);
    //                 return;
    //             }
    //         }
    //     }
    //     public override Character AttackTarget
    //     {
    //         get() =
    //         set(Character character) =
    //     }
    // public override void attack(Character character)
    // {
    //     if (Random.Range(1, 10) == 1) { Houtyou.Add(this, character); }
    //     var hpdayo = character.hp;
    //     character.hp = hpdayo - 5;
    // }

    // public class gagoiruComparer : IComparer<Character>
    // {
    //     public gagoiruComparer(Character chara)
    //     {
    //         thisCCharacter = chara;
    //     }
    //     private Character thisCCharacter;
    //     public int Compare(Character v1, Character v2)
    //     {
    //         var distance1 = charaGetDistantSq(thisCCharacter, v1);
    //         var distance2 = charaGetDistantSq(thisCCharacter, v2);
    //         if (distance1 < distance2) { return -1; }
    //         return 1;
    //     }
    // }
}