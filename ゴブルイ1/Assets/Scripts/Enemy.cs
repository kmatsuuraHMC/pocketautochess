using UnityEngine;
using System.Collections;

/// 敵
public class Enemy : Token
{

    string name = "void";

    public static int Count = 0;
    /// 開始
    void Start()
    {
        Count = Count + 1;
        // ランダムな方向に移動する
        // 方向をランダムに決める
        float dir = Random.Range(0, 359);
        // 速さは2
        float spd = 2;
        SetVelocity(dir, spd);
    }
    void Update()
    {
        // カメラの左下座標を取得
        Vector2 min = GetWorldMin();
        // カメラの右上座標を取得する
        Vector2 max = GetWorldMax();

        if (tokenX < min.x || max.x < tokenX)
        {
            // 画面外に出たので、X移動量を反転する
            VX *= -1;
            // 画面内に移動する
            ClampScreen();
        }
        if (tokenY < min.y || max.y < tokenY)
        {
            // 画面外に出たので、Y移動量を反転する
            VY *= -1;
            // 画面内に移動する
            ClampScreen();
        }
    }
    /// クリックされた
    public void OnMouseDown()
    {
        Count = Count - 1;
        // パーティクルを生成
        for (int i = 0; i < 32; i++)
        {
            Particle.Add(tokenX, tokenY);
        }
        // 破棄する
        DestroyObj();
    }
}