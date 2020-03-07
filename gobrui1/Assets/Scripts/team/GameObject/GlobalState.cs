using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using S = System;
public class GlobalState : GlobalStateF.GlobalState
{
    public void Update()
    {
        this.get_UpdateFunc();
    }
    public new void Start()
    {
        this.get_Start();
    }
}