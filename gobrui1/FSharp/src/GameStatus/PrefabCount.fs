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
        | Houtyou -> "Houtyou"
        | Explosion -> "Explosion"
        | Cheese -> "Cheese"
        | Gobrui -> "Gobrui"
        | Gagoiru -> "Gagoiru"
        | Maruta -> "Maruta"
