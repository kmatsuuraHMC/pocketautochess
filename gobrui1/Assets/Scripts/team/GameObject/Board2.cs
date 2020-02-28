using UnityEngine;
public class Board2 : Token
{
    public void OnMouseDown()
    {
        BackBoard.deployStatus = BackBoard.DeployStatus.team2;
    }
    private void OnMouseUp()
    {
        BackBoard.deployStatus = BackBoard.DeployStatus.none;
    }
    public void OnMouseOver()
    {
        if (BackBoard.deployStatus == BackBoard.DeployStatus.team1) { BackBoard.deployStatus = BackBoard.DeployStatus.none; }
    }
}