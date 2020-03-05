using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using S = System;
public class BackBoard : GlobalState.GlobalState
{
    // public static DeployStatus deployStatus = DeployStatus.none;
    // static int number1 = 0, number2 = 0;
    // public static Team team1 = new Team(), team2 = new Team();
    // static Vector2 point1, point2;
    // public static Turn turn = Turn.deploy;
    // private const int MAX_UNIT = 30;

    // public string race = "Maruta";
    // void Start()
    // {
    //     race = "Maruta";
    // }
    // void Update()
    // {
    //     if (Input.GetKey(KeyCode.A)) { race = "Gobrui"; return; }
    //     if (Input.GetKey(KeyCode.S)) { race = "Maruta"; return; }
    //     if (Input.GetKey(KeyCode.D)) { race = "Gagoiru"; return; }
    //     switch (turn)
    //     {
    //         case Turn.deploy:
    //             Debug.Log(turn.ToString() + "  " + deployStatus.ToString());
    //             if (number1 > MAX_UNIT && number2 > MAX_UNIT) { turn++; return; };
    //             if (deployStatus != DeployStatus.none)
    //             {
    //                 point2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    //             }
    //             if (MathUtil.distance2(point1, point2) > 1)
    //             {
    //                 point1 = point2;
    //                 var isStatusTeam1 = deployStatus == DeployStatus.team1;
    //                 var whichNumber = isStatusTeam1 ? number1 : number2;
    //                 if (whichNumber > MAX_UNIT) { return; }
    //                 var myteam = isStatusTeam1 ? team1 : team2;
    //                 var opponentTeam = isStatusTeam1 ? team2 : team1;
    //                 var addingChara = Character.Deploy(point1.x, point1.y, whichNumber, race, myteam, opponentTeam);
    //                 myteam.teamMates.Add(addingChara);
    //                 if (deployStatus == DeployStatus.team1) { number1++; } else { number2++; }
    //             }
    //             return;
    //         case Turn.battle:
    //             if (team1.teamMates.Count == 0 || team2.teamMates.Count == 0) { turn = Turn.finished; }
    //             return;
    //         default: return;
    //     }
    // }
    // public enum Turn
    // {
    //     deploy,
    //     battle,
    //     finished
    // }
    // public enum DeployStatus
    // {
    //     team1,
    //     team2,
    //     none
    // }
}