namespace Vector3Util

open UnityEngine


module OperatorV2 =
    let (@*) a (v: Vector2) = Vector2(a * v.x, a * v.y)
    let negate2 v: Vector2 = (-1.0f) @* v
    let (@+) (v: Vector2) (w: Vector2) = Vector2(v.x + w.x, v.y + w.y)
    let (@-) v (w: Vector2) = v @+ (negate2 w)
    let toV3 (v:Vector2):Vector3= Vector3(v.x,v.y,0.0f)

module OperatorV3 =
    let (@@*) (a: float32) (v: Vector3) = Vector3(a * v.x, a * v.y, a * v.z)
    let negate3 v = (-1.0f) @@* v
    let (@@+) (v: Vector3) (w: Vector3) = Vector3(v.x + w.x, v.y + w.y, v.z + w.z)
    let (@@-) (v, w) = (@@+) v (negate3 w)
    let toV2 (v:Vector3):Vector2 = Vector2(v.x,v.y)