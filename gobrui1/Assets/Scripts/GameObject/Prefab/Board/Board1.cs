using UnityEngine;
public class Board1 : BoardsF.Board1
{
    public Board1(BoardsF.BoardController hoge) : base(hoge) { }
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