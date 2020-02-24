using UnityEngine;
using System.Collections;

/// パーティクル
public class BackBoard : Token
{

    public float speed = 2;
    Vector2 vec;
    public void OnMouseDown()
    {
        vec = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("x=" + vec.x + " y=" + vec.y);
        Gobrui.Add(vec.x, vec.y);
    }
}