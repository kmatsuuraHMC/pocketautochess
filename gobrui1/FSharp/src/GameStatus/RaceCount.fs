namespace PrefabCount

type PrefabCount =
    | Gobrui = 0
    | Gagoiru = 1
    | Maruta = 2
    | Explosion = 3

module PrefabCountUtil =
    let toPrefabName prefabCount =
        match prefabCount with
        | PrefabCount.Gobrui -> "Gobrui"
        | PrefabCount.Gagoiru -> "Gagoiru"
        | PrefabCount.Maruta -> "Maruta"
        | PrefabCount.Explosion -> "Explosion"
        | _ -> "Gobrui"
