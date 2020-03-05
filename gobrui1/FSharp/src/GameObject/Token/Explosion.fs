namespace ExplosionF

open UnityEngine
open TokenF
open System.Runtime
open System.Threading.Tasks
open System.Threading

/// パーティクル
type Explosion() =
    inherit Token()
    member this.StartAt: Async<Unit> =
        this.CreateInstance2(this.tokenX, this.tokenY, "explosion") |> ignore
        async {
            while (this.ScaleX > 0.01f) do
                do! Async.AwaitTask (Task.Delay 10)
                // だんだん小さくするf
                this.MulScale(0.9f)
            Debug.Log("hoge")
        }

//     /// パーティクルの生成
//     public static Explosion Add(Character opponent)
//     {
//         // プレハブを取得
//         var _prefab =TokenF.tokenf.GetPrefab("Explosion");
//         var explosion =TokenF.tokenf.CreateInstance2<Explosion>(_prefab, opponent.tokenX, opponent.tokenY, "Explosion");
//         // プレハブからインスタンスを生成
//         return explosion;
//     }

//     /// 開始。コルーチンで処理を行う
//     IEnumerator Start()
//     {

//         // 見えなくなるまで小さくする
//         while (ScaleX > 0.01f)
//         {
//             // 0.01秒ゲームループに制御を返す
//             yield return new WaitForSeconds(0.01f);
//             // だんだん小さくする
//             MulScale(0.9f);
//             // だんだん減速する
//         }
//         // 消滅
//         DestroyObj();
//     }
// }
