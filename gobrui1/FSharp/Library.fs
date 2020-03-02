namespace Library1

open UnityEngine

type TestScript() = 
    inherit MonoBehaviour()
    member this.Start() = Debug.Log("Hello F# World")