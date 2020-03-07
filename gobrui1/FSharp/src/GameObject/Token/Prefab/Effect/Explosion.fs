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
open UniRx.Async

/// パーティクル
type Explosion() =
    inherit Token()
    let mutable i = 0

    member this.I
        with get () = i
        and set v = i <- v

    member this.StartFunc = this.ShrinkOut 0.8f

    static member Add(attacktarget: Character) =
        let prefab = GetPrefab null PrefabCount.Explosion
        let expro = CreateInstance2<Explosion>(prefab, attacktarget.tokenX, attacktarget.tokenY, "Explosion")
        expro
