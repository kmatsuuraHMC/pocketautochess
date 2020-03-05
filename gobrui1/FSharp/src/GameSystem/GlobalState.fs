namespace BackBoard
// ココらへん、制御構造を書き直す必要がある
open UnityEngine
open TeamF
open TokenF

type Turn =
    | deploy = 0
    | battle = 1
    | finished = 2

type DeployStatus =
    | none = 0
    | team1 = 1
    | team2 = 2

type GlobalState() =
    inherit MonoBehaviour()
    let mutable deployStatus = DeployStatus.none
    let mutable turn = Turn.deploy
    let mutable number1, number2 = 0, 0
    let mutable point1, poin2 = Vector2(0.0f, 0.0f), Vector2(0.0f, 0.0f)
    let mutable team1, team2 = Team(), Team()
    let MAX_UNIT = 30
    let race= "Maruta"

//     void Start()
//     {
//         race = "Maruta";
//     }
//     void Update()
//     {
//         if (Input.GetKey(KeyCode.A)) { race = "Gobrui"; return; }
//         if (Input.GetKey(KeyCode.S)) { race = "Maruta"; return; }
//         if (Input.GetKey(KeyCode.D)) { race = "Gagoiru"; return; }
//         switch (turn)
//         {
//             case Turn.deploy:
//                 Debug.Log(turn.ToString() + "  " + deployStatus.ToString());
//                 if (number1 > MAX_UNIT && number2 > MAX_UNIT) { turn++; return; };
//                 if (deployStatus != DeployStatus.none)
//                 {
//                     point2 = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//                 }
//                 if (MathUtil.distance2(point1, point2) > 1)
//                 {
//                     point1 = point2;
//                     var isStatusTeam1 = deployStatus == DeployStatus.team1;
//                     var whichNumber = isStatusTeam1 ? number1 : number2;
//                     if (whichNumber > MAX_UNIT) { return; }
//                     var myteam = isStatusTeam1 ? team1 : team2;
//                     var opponentTeam = isStatusTeam1 ? team2 : team1;
//                     var addingChara = Character.Deploy(point1.x, point1.y, whichNumber, race, myteam, opponentTeam);
//                     myteam.teamMates.Add(addingChara);
//                     if (deployStatus == DeployStatus.team1) { number1++; } else { number2++; }
//                 }
//                 return;
//             case Turn.battle:
//                 if (team1.teamMates.Count == 0 || team2.teamMates.Count == 0) { turn = Turn.finished; }
//                 return;
//             default: return;
//         }
//     }
