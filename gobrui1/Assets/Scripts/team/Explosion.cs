using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniRx.Async;
/// パーティクル
public class Explosion : ExplosionF.Explosion
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
    public float xnosukeru
    {
        get
        {
            return this.transform.localScale.x;
        }
        set
        {
            Vector3 scale = transform.localScale;
            scale.x = value;
        }
    }

    public float ynosuke
    {
        get
        {
            return this.transform.localScale.y;
        }
        set
        {
            var scale = transform.localScale;
            scale.y = value;
            this.transform.localScale = scale;
        }
    }
    // public async void Start()
    // {
        // await UniTask.Run(() =>
        // {
        //     while (true)
        //     {
        //         this.ynosuke = this.ynosuke * 0.01f;
        //     }

        // });
    // }
}