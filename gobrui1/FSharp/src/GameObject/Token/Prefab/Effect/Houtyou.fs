namespace HoutyouF

open UnityEngine
open TokenF
open tokenUtil
open PrefabCount
open System.Collections
/// パーティクル
type Houtyou() =
    inherit Token()
    member this.Start() = this.ShrinkOut 0.8f
    /// コルーチンでフェードアウト(CSharpで設定してる)
    static member Add (rancher: Token) (opponent: Token) =
        let prefab = GetPrefab null PrefabCount.Houtyou
        let houtyou = CreateInstance2<Houtyou>(prefab, rancher.tokenX, rancher.tokenY, "Houtyou")
        houtyou.SetVelocity(opponent.tokenX - rancher.tokenX, opponent.tokenY - rancher.tokenY, 9.0f)
        houtyou
