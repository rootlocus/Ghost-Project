using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Usable
{
    protected override void Start() 
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        //if(Input.GetKeyUp(KeyCode.Space))
        //{
        //    this.onUse();
        //}
    }
    
    protected override void execute()
    {
        Debug.Log("Potion Used");
    }
}
