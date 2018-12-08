using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour {
    public Level1Player player;
    public Level1BastOther bast_other;
    public float zoom = 2f;
    private Level1Other[] others;
    void Awake()
    {
        player.gameObject.SetActive(true);
        others = GameObject.FindObjectsOfType<Level1Other>();
        foreach (var other in others)
        {
            other.gameObject.SetActive(true);
        }
    }

	void Update ()
    {
        var pos = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
        pos.z = transform.position.z;
        transform.position = pos;
    }
}
