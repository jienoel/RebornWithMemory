using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BastOther : Obj
{
    public Level1 mgr;
	
	void Update ()
    {
        var dist = Vector3.Distance(mgr.player.transform.position, transform.position);
        if(dist < mgr.closeDist)
        {
            mgr.Find();
        }
    }
}
