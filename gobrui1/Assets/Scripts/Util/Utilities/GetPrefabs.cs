using UnityEngine;
using System.Collections;

public class GetPrefabFunction
{
    public static GameObject GetPrefab(GameObject prefab, string name)
    {
        name = null;
        return prefab ?? (prefab = Resources.Load("Prefabs/" + name) as GameObject);
    }
}
