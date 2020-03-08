namespace ExplosionF

open UnityEngine
open TokenF
open tokenUtil
open PrefabCount
open TeamF
open System.Runtime
open System.Threading.Tasks
open System.Threading
open System.Collections

/// パーティクル
type Explosion() =
    inherit Token()

    member this.StartFunc = this.ShrinkOut 0.8f

    /// コルーチンでフェードアウト(CSharpで設定してる)
    static member Add(attacktarget: Character) =
        let prefab = GetPrefab null PrefabCount.Explosion
        let expro = CreateInstance2<Explosion>(prefab, attacktarget.tokenX, attacktarget.tokenY, "Explosion")
        expro
