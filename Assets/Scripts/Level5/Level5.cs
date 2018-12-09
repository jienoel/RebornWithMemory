using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level5 : MonoBehaviour {

    public Level5Player player;

    public GameObject endPoint;

    public bool hasFinded = false;

    public GameObject sceneLoader;

    private Material bgMat;
    public float animTime = 1f;
    private float animTimeCount = 0f;

    void Awake () {
        player.mgr = this;
        bgMat = transform.Find("bg").GetComponent<SpriteRenderer>().material;
    }
	
	// Update is called once per frame
	void Update () {
        var pos = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
        pos.z = transform.position.z;
        transform.position = pos;

        if (!hasFinded && player.transform.position.y > endPoint.transform.position.y)
        {
            hasFinded = true;
            sceneLoader.SetActive(true);
        }

        if (hasFinded && animTimeCount > -1f)
        {
            animTimeCount += Time.deltaTime;
            animTimeCount = Mathf.Clamp(animTimeCount, 0, animTime);
            bgMat.SetFloat("_Blend", animTimeCount / animTime);

            if (animTimeCount >= animTime)
            {
                animTimeCount = -111;
                sceneLoader.SetActive(true);
            }
        }
    }
}
