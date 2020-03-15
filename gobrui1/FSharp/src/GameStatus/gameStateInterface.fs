namespace gameStateInterface

open UnityEngine

type GameStateInterface =
    interface
    let mutable previousPutPosition = Vector2(0.0f, 0.0f)
    let mutable Team1 = Team()
    let mutable Team2 = Team()