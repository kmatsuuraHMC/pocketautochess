using UnityEngine;
public class Board2 : BoardsF.Board2
{
    public Board2(BoardsF.BoardController hoge) : base(hoge) { }
    public void OnMouseDown()
    {
        this.get_OnMouseDownFunc();
    }
    public void OnMouseUp()
    {
        this.get_OnMouseUpFunc();
    }
    public void OnMouseOver()
    {
        this.get_OnMouseOverFunc();
    }
}