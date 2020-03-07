namespace CheeseF

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
type Cheese() =
    inherit Token()
    let mutable i = 0

    member this.I
        with get () = i
        and set v = i <- v

    member this.StartFunc = this.ShrinkOut 0.7f

    static member Add(attacktarget: Character) =
        let prefab = GetPrefab null PrefabCount.Cheese
        let expro = CreateInstance2<Cheese>(prefab, attacktarget.tokenX, attacktarget.tokenY, "Cheese")
        expro
