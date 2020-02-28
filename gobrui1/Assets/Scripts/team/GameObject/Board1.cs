using UnityEngine;
public class Board1 : Token
{
    public void OnMouseDown()
    {
        BackBoard.deployStatus = BackBoard.DeployStatus.team1;
    }
    private void OnMouseUp()
    {
        BackBoard.deployStatus = BackBoard.DeployStatus.none;
    }
    public void OnMouseOver()
    {
        if (BackBoard.deployStatus == BackBoard.DeployStatus.team2) { BackBoard.deployStatus = BackBoard.DeployStatus.none; }
    }
}