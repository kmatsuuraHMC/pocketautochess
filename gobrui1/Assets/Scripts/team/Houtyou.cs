using UnityEngine;
using System.Collections;
using S = System;

/// パーティクル
public class Houtyou : Token
{
    static GameObject _prefab = null;
    /// パーティクルの生成
    public static Houtyou Add(Character rancher, Character opponent)
    {
        // プレハブを取得
        _prefab = TokenF.tokenf.GetPrefab("Houtyou");
        var houtyou = TokenF.tokenf.CreateInstance2<Houtyou>(_prefab, rancher.tokenX, rancher.tokenY, "Houtyou");
        houtyou.SetVelocity(opponent.tokenX - rancher.tokenX, opponent.tokenY- rancher.tokenY,10);
        // プレハブからインスタンスを生成
        return houtyou;
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
