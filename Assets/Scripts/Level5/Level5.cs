using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour {

    public Level5Player player;

    public GameObject endPoint;

    public float closeDist = 0.1f;

    public bool hasFinded = false;

    public GameObject sceneLoader;

    void Awake () {
        player.mgr = this;
    }
	
	// Update is called once per frame
	void Update () {
        if (hasFinded)
        {
            return;
        }

        var pos = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
        pos.z = transform.position.z;
        transform.position = pos;

        if (Vector3.Distance(player.transform.position, endPoint.transform.position) < closeDist)
        {
            hasFinded = true;
            sceneLoader.SetActive(true);
        }
	}
}
