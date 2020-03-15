namespace PreActionDriver

open UnityEngine
open BoardsF
open GlobalStateI
open PrefabCount

module PreActionDriver=
    let PreActionDriver () =
        if (Input.GetKey(KeyCode.A)) then BoardController.Prefab <- PrefabCount.Gobrui
        if (Input.GetKey(KeyCode.S)) then BoardController.Prefab <- PrefabCount.Maruta
        if (Input.GetKey(KeyCode.D)) then BoardController.Prefab <- PrefabCount.Gagoiru