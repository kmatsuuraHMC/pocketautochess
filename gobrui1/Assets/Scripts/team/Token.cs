using UnityEngine;
using S = System;

/// キャラクター基底クラス.
/// SpriteRendererが必要.
[RequireComponent(typeof(SpriteRenderer))]
public class Token : MonoBehaviour
{
    public float tokenX
    {
        set
        {
            Vector3 pos = transform.position;
            pos.x = value;
            transform.position = pos;
        }
        get { return transform.position.x; }
    }
    /// 座標(Y).
    public float tokenY
    {
        set
        {
            Vector3 pos = transform.position;
            pos.y = value;
            transform.position = pos;
        }
        get { return transform.position.y; }
    }
    /// プレハブ取得.
    /// プレハブは必ず"Resources/Prefabs/"に配置すること.
    public static GameObject GetPrefab(GameObject prefab, string name)
    {
        return prefab ?? (prefab = Resources.Load("Prefabs/" + name) as GameObject);
    }

    /// インスタンスを生成してスクリプトを返す.
    public static Type CreateInstance<Type>(GameObject prefab, Vector3 p, string name) where Type : Token
    {
        GameObject g = Instantiate(prefab, p, Quaternion.identity) as GameObject;
        Type obj = g.GetComponent<Type>();
        g.name = name;
        return obj;
    }

    public static Type CreateInstance2<Type>(GameObject prefab, float x, float y, string name) where Type : Token
    {
        Vector3 pos = new Vector3(x, y, 0);
        return CreateInstance<Type>(prefab, pos, name);
    }

    /// アクセサ.
    /// レンダラー.
    SpriteRenderer _renderer = null;

    public SpriteRenderer Renderer
    {
        get { return _renderer ?? (_renderer = gameObject.GetComponent<SpriteRenderer>()); }
    }

    /// 剛体.
    Rigidbody2D _rigidbody2D = null;

    public Rigidbody2D RigidBody
    {
        get { return _rigidbody2D ?? (_rigidbody2D = gameObject.GetComponent<Rigidbody2D>()); }
    }

    /// 移動量を設定.
    public void SetVelocity(float x, float y, float speed)
    {
        float absoluteVelocity = Mathf.Sqrt(x * x + y * y);
        Vector2 v;
        v.x = x / absoluteVelocity * speed;
        v.y = y / absoluteVelocity * speed;
        RigidBody.velocity = v;
    }
    public void DestroyObj()
    {
        Destroy(gameObject);
    }
    float _width = 0.0f;
    float _height = 0.0f;

    /// 移動して画面内に収めるようにする.
    public void ClampScreenAndMove(Vector2 v)
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        pos += v;

        // 画面内に収まるように制限をかける.
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // プレイヤーの座標を反映.
        transform.position = pos;
    }

    /// 画面内に収めるようにする.
    public void ClampScreen()
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        // 画面内に収まるように制限をかける.
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // プレイヤーの座標を反映.
        transform.position = pos;
    }

    /// 画面外に出たかどうか.
    public bool IsOutside()
    {
        Vector2 min = GetWorldMin();
        Vector2 max = GetWorldMax();
        Vector2 pos = transform.position;
        if (pos.x < min.x || pos.y < min.y)
        {
            return true;
        }
        if (pos.x > max.x || pos.y > max.y)
        {
            return true;
        }
        return false;
    }

    /// 画面の左下のワールド座標を取得する.
    public Vector2 GetWorldMin(bool noMergin = false)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(Vector2.zero);
        if (noMergin)
        {
            // そのまま返す.
            return min;
        }

        // 自身のサイズを考慮する.
        min.x += _width;
        min.y += _height;
        return min;
    }

    /// 画面右上のワールド座標を取得する.
    public Vector2 GetWorldMax(bool noMergin = false)
    {
        Vector2 max = Camera.main.ViewportToWorldPoint(Vector2.one);
        if (noMergin)
        {
            // そのまま返す.
            return max;
        }

        // 自身のサイズを考慮する.
        max.x -= _width;
        max.y -= _height;
        return max;
    }


    /// スケール値(X).
    public float ScaleX
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
            transform.localScale = scale;
        }
        get { return transform.localScale.x; }
    }

    /// スケール値(Y).
    public float ScaleY
    {
        set
        {
            Vector3 scale = transform.localScale;
            scale.y = value;
            transform.localScale = scale;
        }
        get { return transform.localScale.y; }
    }

    // rigidbodyあたり
    /// スケール値をかける.
    public void MulScale(float d)
    {
        transform.localScale *= d;
    }

    /// 移動量をかける.
    public void MulVelocity(float d)
    {
        RigidBody.velocity *= d;
    }

}
