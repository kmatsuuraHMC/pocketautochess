namespace ExplosionF

open UnityEngine
open TokenF
open System.Runtime
open System.Threading.Tasks
open System.Threading
open UniRx.Async

/// パーティクル
type Explosion() =
    inherit Token()
// member this.startfunc:Async<UniTask> =
//     async {
//         let span = UniTask.Delay((int) (50))
//         let awaiter = span.GetAwaiter()
//         while (this.ScaleX > 0.01f) do
//             this.MulScale(0.9f)
//             do! span // 一歩ごとに待機
//         this.DestroyObj()
//     }
