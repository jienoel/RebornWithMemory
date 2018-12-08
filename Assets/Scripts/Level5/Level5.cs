using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour {

    public Level5Player player;

    public GameObject endPoint;

    void Awake () {

    }
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(player.transform.position, endPoint.transform.position) < 0.5)
        {
            Debug.Log("通关");
        }
	}
}
