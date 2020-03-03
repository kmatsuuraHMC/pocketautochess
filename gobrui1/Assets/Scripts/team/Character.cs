using UnityEngine;
using System.Collections;
using S = System;
using System.Collections.Generic;
using Microsoft.FSharp.Core;
using System.Linq;

/// 敵
public class Character : Token
{
    public float hp;
    public float direction = 0; /// 向き
    public int number;
    public Character attackTarget;
    public Team myTeam, opponentTeam;
    public static Character Deploy(float xIn, float yIn, int numberIn, string race, Team toSetmyTeam, Team toSetOpponentTeam)
    {
        var _prefab = TokenF.tokenf.GetPrefab(race);
        var chara = AddWithRace(xIn, yIn, numberIn, race);
        chara.number = numberIn;
        chara.myTeam = toSetmyTeam;
        chara.opponentTeam = toSetOpponentTeam;
        chara.myTeam.teamMates.Add(chara);
        return chara;
    }
    private static Character AddWithRace(float xIn, float yIn, int numberIn, string race)
    {
        GameObject _prefab = TokenF.tokenf.GetPrefab(race);
        switch (race)
        {
            case "Gobrui": return TokenF.tokenf.CreateInstance2<Gobrui>(_prefab, xIn, yIn, race + numberIn.ToString()).ToCharacter();
            case "Maruta": return TokenF.tokenf.CreateInstance2<Maruta>(_prefab, xIn, yIn, race + numberIn.ToString()).ToCharacter();
            case "Gagoiru": return TokenF.tokenf.CreateInstance2<Gagoiru>(_prefab, xIn, yIn, race + numberIn.ToString()).ToCharacter();
            default: return null;
        };
    }
    public void attacked(int attackPoint)
    {
        hp = hp - attackPoint;
    }
    virtual public void attack(Character character, float attackPoint)
    {
        var hpdayo = character.hp;
        character.hp = hpdayo - attackPoint;
    }
    public Character ToCharacter()
    {
        return this;
    }
    public float getDistanceSqToOpponent(Character opponent)
    {
        return CharaUtil.charaGetDistantSq(this, opponent);
    }
    public virtual Character chooseAttackTarget(List<Character> teamMates, Character chara)
    {
        var xyn_c = new xynComparer(chara);
        var teamMates_xyn = new List<Character>(teamMates);
        teamMates_xyn.Min();
        var chosen = teamMates_xyn[0];
        return chosen;
    }
    public static S.Func<Character, Character, float> charaGetDistantSq = (v1, v2) => CharaUtil.getDistantSq((v1.tokenX, v1.tokenY), (v2.tokenX, v2.tokenY));
    public class xynComparer : IComparer<Character>
    {
        public xynComparer(Character chara)
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