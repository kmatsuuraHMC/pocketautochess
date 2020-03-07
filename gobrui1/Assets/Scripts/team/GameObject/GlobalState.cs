using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using S = System;
public class GlobalState : GlobalStateF.GlobalState
{
    public void Update()
    {
        Debug.Log("board2mousedown");
        Debug.Log("board2mousedown");
        this.get_UpdateFunc();
    }
    public new void Start()
    {
        this.get_Start();
    }
}