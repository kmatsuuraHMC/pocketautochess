namespace PrefabCount

type PrefabCount =
    | Explosion = 3
    | Cheese = 5
    | Houtyou = 4
    | Gobrui = 0
    | Gagoiru = 1
    | Maruta = 2


module PrefabCountUtil =
    let toPrefabName prefabCount =
        match prefabCount with
        | PrefabCount.Houtyou -> "Houtyou"
        | PrefabCount.Explosion -> "Explosion"
        | PrefabCount.Cheese -> "Cheese"
        | PrefabCount.Gobrui -> "Gobrui"
        | PrefabCount.Gagoiru -> "Gagoiru"
        | PrefabCount.Maruta -> "Maruta"
        | PrefabCount.Houtyou -> "Houtyou"
        | _ -> "Gobrui"
