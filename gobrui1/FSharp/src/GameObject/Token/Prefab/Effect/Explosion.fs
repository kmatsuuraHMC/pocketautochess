namespace ExplosionF

open UnityEngine
open TokenF
open System.Runtime
open System.Threading.Tasks
open System.Threading

/// パーティクル
type Explosion() =
    inherit Token()

    member this.StartFunc =
        new Task(fun () ->
        Debug.Log("hoge")
        this.MulScale(0.3f)
        while (true) do
            this.DestroyObj()
            Debug.Log("Fuga")
            this.MulScale(0.3f)
        )
    // だんだん小さくするf

    static member Add(opponent: Token) =
        tokenUtil.CreateInstance2(null, opponent.tokenX, opponent.tokenY, "explosion") |> ignore

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
//     }
// }
