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

/// パーティクル
type Cheese() =
    inherit Token()
    let mutable i = 0
    member this.Start() = this.ShrinkOut 0.8f

    member this.I
        with get () = i
        and set v = i <- v

    member this.StartFunc = this.ShrinkOut 0.7f

    static member Add(attacktarget: Character) =
        /// コルーチンでフェードアウト(CSharpで設定してる)
        let prefab = GetPrefab null PrefabCount.Cheese

        let expro = CreateInstance2<Cheese>(prefab, attacktarget.pos, "Cheese")
        expro
