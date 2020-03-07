namespace TokenF

open UnityEngine
open Vector3Util.OperatorV2
open Vector3Util.OperatorV3
open tokenUtil

type Token() =
    inherit MonoBehaviour()
    let _renderer: SpriteRenderer = null
    let _rigidbody2D: Rigidbody2D = null
    let _width: float32 = 0.0f
    let _height: float32 = 0.0f
    let mutable _prefab: GameObject = null

    member this.tokenX
        with get () = this.transform.position.x
        and set value =
            do let mutable pos = this.transform.position
               pos.x <- value
               this.transform.position <- pos

    member this.tokenY
        with get () = this.transform.position.y
        and set value =
            do let mutable pos = this.transform.position
               pos.y <- value
               this.transform.position <- pos

    member this.Renderer: SpriteRenderer =
        match _renderer with
        | null ->
            let _renderer = this.gameObject.GetComponent<SpriteRenderer>()
            _renderer
        | _ -> _renderer

    member this.RigidBody: Rigidbody2D =
        match _rigidbody2D with
        | null ->
            let _rigidbody2D = this.gameObject.GetComponent<Rigidbody2D>()
            _rigidbody2D
        | _ -> _rigidbody2D

    member this.SetVelocity(x, y, speed) =
        do let absoluteVelocity = Mathf.Sqrt(x * x + y * y)
           let v = Vector2(x / absoluteVelocity * speed, y / absoluteVelocity * speed)
           this.RigidBody.velocity <- v

    member this.DestroyObj() = GameObject.Destroy(this.gameObject)

    member this.MulScale(d: float32): Unit = this.transform.localScale <- d  @@* this.transform.localScale //d

    member this.MulVelocity(d: float32): Unit = this.RigidBody.velocity <- d @* this.RigidBody.velocity

    /// 画面の左下のワールド座標を取得する.
    member this.GetWorldMin(noMergin: Option<bool>): Vector2 =
        let valNoMergin =
            match noMergin with
            | Some a -> a
            | None -> false

        let min = Camera.main.ViewportToWorldPoint(Vector3.zero)
        if (valNoMergin) then Vector2(min.x, min.y) else Vector2(min.x + _width, min.y + _height)

    member this.GetWorldMax(noMergin: Option<bool>): Vector2 =
        let valNoMergin =
            match noMergin with
            | Some a -> a
            | None -> false

        let max = Camera.main.ViewportToWorldPoint(Vector3.zero)
        if (valNoMergin) then Vector2(max.x, max.y) else Vector2(max.x - _width, max.y - _height)

    /// 移動して画面内に収めるようにする.
    member this.ClampScreenAndMove(v: Vector2): Unit =
        let mutable (min, max, pos) = (this.GetWorldMax None, this.GetWorldMin None, toV2 (this.transform.position))
        let pos = pos @+ v
        this.transform.position <- Vector3(Mathf.Clamp(pos.x, min.x, max.x), Mathf.Clamp(pos.y, min.y, max.y), 0.0f)

    member this.CloampScreen =
        let mutable (min, max, pos) = (this.GetWorldMin None, this.GetWorldMax None, this.transform.position)
        let (modifiedPosX, modifiedPosY) = (Mathf.Clamp(pos.x, min.x, max.x), Mathf.Clamp(pos.y, min.y, max.y))
        pos <- Vector3(modifiedPosX, modifiedPosY, pos.z)
        this.transform.position <- pos

    member this.ScaleX
        with get () = this.transform.localScale.x
        and set v =
            let mutable scale = this.transform.localScale
            scale.x <- v
            this.transform.localScale <- scale

    member this.ScaleY
        with get () = this.transform.localScale.y
        and set v =
            let mutable scale = this.transform.localScale
            scale.y <- v
            this.transform.localScale <- scale

    member this.IsOutside =
        let (min, max, pos) = (this.GetWorldMin(None), this.GetWorldMax(None), this.transform.position)
        (pos.x < min.x || pos.y < min.y) || (pos.x > max.x || pos.y > max.y)

    member this.Prefab
        with get () = _prefab
        and set (hoge) = _prefab <- hoge

module tokenUtil =

    let GetPrefab prefab name =
        match prefab with
        | null -> Resources.Load("Prefabs/" + name) :?> GameObject
        | _ -> prefab

    let CreateInstance<'Type when 'Type :> Token>(prefab: GameObject, p: Vector3, name: string): 'Type =
        let g = GameObject.Instantiate(prefab, p, Quaternion.identity) :?> GameObject
        let obj: 'Type = g.GetComponent<'Type>()
        g.name <- name
        obj

    let CreateInstance2<'Type when 'Type :> Token>(prefab: GameObject, x, y, name) =
        let pos = Vector3(x, y, 0.0f)
        let g = CreateInstance<'Type>(prefab, pos, name)
        g

    let giveTokenChartAndName (prefab, chartX, chartY, name) =
        let token = Token()
        token.Prefab <- prefab
        token.tokenX <- chartX
        token.tokenY <- chartY
        token.name <- name
        token

    let getDistanceSq (token1: Token, token2: Token) =
        let X = token1.tokenX - token2.tokenX
        let Y = token1.tokenY - token2.tokenY
        X * X + Y * Y
