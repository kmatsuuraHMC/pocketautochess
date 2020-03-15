namespace PrefabCount

type PrefabCount =
    | Explosion
    | Cheese
    | Houtyou
    | Gobrui
    | Gagoiru
    | Maruta


module PrefabCountUtil =
    let toPrefabName prefabCount =
        match prefabCount with
        | PrefabCount.Houtyou -> "Houtyou"
        | PrefabCount.Explosion -> "Explosion"
        | PrefabCount.Cheese -> "Cheese"
        | PrefabCount.Gobrui -> "Gobrui"
        | PrefabCount.Gagoiru -> "Gagoiru"
        | PrefabCount.Maruta -> "Maruta"
