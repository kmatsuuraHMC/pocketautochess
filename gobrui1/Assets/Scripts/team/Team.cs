using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
public class Team
{
    public List<Character> teamMates = new List<Character>();
    public Team()
    {
    }
    public void Add(Character character)
    {
        teamMates.Add(character);
    }
    public static int delete(List<Character> teamMates, int number)
    {
        Character searchedCharaByName= teamMates.Find(chara => chara.number == number);
        for (int i = 0; i < 5; i++)
        {
            Particle.Add(searchedCharaByName.tokenX, searchedCharaByName.tokenY);
        }
        return teamMates.RemoveAll(chara =>
                {
                    var isElase = number == chara.number;
                    if (isElase) { chara.DestroyObj(); }
                    return isElase;
                }
        );
    }
}
