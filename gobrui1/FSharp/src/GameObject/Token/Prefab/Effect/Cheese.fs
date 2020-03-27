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
// open MiniJSON
open UDPManager

/// パーティクル
type Cheese() =
    inherit Token()
    let mutable i = 0
    // let OnReceiveMessage(jsonNode: JsonNode, jsonStr: string) = ()
    member this.Start() = this.ShrinkOut 0.8f

    member this.StartFunc = this.ShrinkOut 0.7f

    static member Add(attacktarget: Character) =
        /// コルーチンでフェードアウト(CSharpで設定してる)
        let prefab = GetPrefab null PrefabCount.Cheese

        let expro = CreateInstance2<Cheese>(prefab, attacktarget.pos, "Cheese")
        expro

    //1.受信処理をするクラスを作成

    //2.関数をUDPManagerに登録
    member this.start() =() // Event.add OnReceiveMessage UDPManager.Instance.messageReceivedI.Publish

//3.オブジェクトが破棄されたときに関数の登録を解除するようにしておく
//OnDestroyはMonoBehaviourが破棄されるときに自動的に呼ばれる関数
// member this.OnDestroy() = UDPManager.Instance.messageReceived -= OnReceiveMessage
