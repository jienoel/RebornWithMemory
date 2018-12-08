using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1BastOther : MonoBehaviour {
    public Level1 mgr;
	
	// Update is called once per frame
	void Update ()
    {
        var dist = Vector3.Distance(mgr.player.transform.position, transform.position);
        if(dist < 0.05f)
        {
            Debug.Log("游戏结束");
        }

    }
}
