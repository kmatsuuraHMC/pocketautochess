using UnityEngine;
using System.Collections;
using S = System;

/// パーティクル
public class Particle
{
    //     static GameObject _prefab = null;
    //     /// パーティクルの生成
    //     public static Particle Add(float x, float y)
    //     {
    //         // プレハブを取得
    //         _prefab = TokenF.tokenf.GetPrefab("Particle");
    //         // プレハブからインスタンスを生成
    //         return TokenF.tokenf.CreateInstance2<Particle>(_prefab, x, y, "Particle");
    //     }

    //     /// 開始。コルーチンで処理を行う
    //     IEnumerator Start()
    //     {
    //         // 移動方向と速さをランダムに決める
    //         float dir = Random.Range(0, 2 * (float)S.Math.PI);
    //         float spd = Random.Range(10.0f, 20.0f);
    //         SetVelocity((float)S.Math.Cos(dir), (float)S.Math.Sin(dir), spd);


    //         // 見えなくなるまで小さくする
    //         while (ScaleX > 0.01f)
    //         {
    //             // 0.01秒ゲームループに制御を返す
    //             yield return new WaitForSeconds(0.01f);
    //             // だんだん小さくする
    //             MulScale(0.9f);
    //             // だんだん減速する
    //             MulVelocity(0.9f);
    //         }
    //         // 消滅
    //         DestroyObj();
    //     }
}
