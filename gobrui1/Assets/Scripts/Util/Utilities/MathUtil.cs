using UnityEngine;
using System.Collections;

public class MathUtil
{

    /// 入力方向を取得する.
    public static Vector2 GetInputVector()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        return new Vector2(x, y).normalized;
    }
    public static float distance2(Vector2 vec, Vector2 vec2)
    {
        float diffX = vec.x - vec2.x;
        float diffY = vec.y - vec2.y;
        return diffX * diffX + diffY * diffY;
    }
}