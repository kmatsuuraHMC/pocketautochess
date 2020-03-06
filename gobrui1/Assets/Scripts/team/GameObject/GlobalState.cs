using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using S = System;
public class GlobalState : GlobalStateF.GlobalState
{
    public new void Update()
    {
        //this.get_Update(); 
    }
    public new void Start()
    {
        this.get_Start();
    }
}