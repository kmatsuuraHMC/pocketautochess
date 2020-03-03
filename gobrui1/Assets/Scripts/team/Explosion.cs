using UnityEngine;
using System.Collections;
using S = System;

/// パーティクル
public class Explosion : Token
{
    /// パーティクルの生成
    public static Explosion Add(Character opponent)
    {
        // プレハブを取得
        var _prefab =TokenF.tokenf.GetPrefab("Explosion");
        var explosion =TokenF.tokenf.CreateInstance2<Explosion>(_prefab, opponent.tokenX, opponent.tokenY, "Explosion");
        // プレハブからインスタンスを生成
        return explosion;
    }

    /// 開始。コルーチンで処理を行う
    IEnumerator Start()
    {

        // 見えなくなるまで小さくする
        while (ScaleX > 0.01f)
        {
            // 0.01秒ゲームループに制御を返す
            yield return new WaitForSeconds(0.01f);
            // だんだん小さくする
            MulScale(0.9f);
            // だんだん減速する
        }
        // 消滅
        DestroyObj();
    }
}
