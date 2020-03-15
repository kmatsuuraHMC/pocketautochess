namespace GlobalStateI

open TeamF
open UnityEngine

type GlobalStateI =
    interface
        abstract previousPutPosition: Vector2 with get, set
        abstract Team1: Team
        abstract Team2: Team
    end
