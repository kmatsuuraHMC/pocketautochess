using UnityEngine;
using System.Collections;

public class Gobrui : Token
{
    private void Start()
    {

    }
    private void Update()
    {

    }
    static GameObject _prefab = null;
    public static Gobrui Add(float x, float y)
    {
        _prefab = GetPrefab(_prefab, "Gobrui");
        // プレハブからインスタンスを生成
        return CreateInstance2<Gobrui>(_prefab, x, y);
    }

}