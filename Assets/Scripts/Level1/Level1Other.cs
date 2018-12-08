using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Other : MonoBehaviour {
    public Level1 mgr;
    public float force_size = 0.01f;

	void Update ()
    {
        var dist_me = Vector3.Distance(transform.position, mgr.player.transform.position);
        if (dist_me < mgr.zoom)
        {
            var pos = mgr.player.transform.position 
                + (transform.position - mgr.player.transform.position).normalized * mgr.zoom;
            transform.position = Vector3.Lerp(transform.position, pos, force_size);
        }
    }
}
