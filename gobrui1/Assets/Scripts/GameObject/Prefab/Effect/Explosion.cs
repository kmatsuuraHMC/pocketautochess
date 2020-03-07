using Microsoft.FSharp.Control;
using Microsoft.FSharp.Core;
using System.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UniRx.Async;
using System.Collections;
/// パーティクル
public class Explosion : ExplosionF.Explosion
{
    public IEnumerator Start()
    {
        return this.StartFunc;
    }
}