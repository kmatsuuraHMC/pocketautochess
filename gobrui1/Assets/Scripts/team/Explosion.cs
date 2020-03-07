using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniRx.Async;
using System.Collections;
/// パーティクル
public class Explosion : ExplosionF.Explosion
{
    public async void Start()
    {
        await Move();

        Debug.Log("Done!");  // Move() の処理完了後に実行される
    }

    private async UniTask Move()
    {

        float speed = 1f;
        float duration = 3f;
        var startTime = Time.time;

        // 待機時間（UniTask.Delay に変換。 引数にはミリ秒の int を指定する）
        var span = UniTask.Delay((int)(50));

        span.GetAwaiter();

        while (this.ScaleX > 0.01f)
        {
            this.MulScale(0.9f);
            await span;       // 一歩ごとに待機
        }
        this.DestroyObj();
    }
}