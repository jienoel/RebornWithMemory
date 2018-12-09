using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level6 : MonoBehaviour {

    public Level6Player player;

    public float zoom = 2f;
    public bool hasFinded = false;
    private Material bgMat;
    public float animTime = 1f;
    private float animTimeCount = 0f;

    public GameObject sceneLoader;

    public int minCount = 10;
    private int count = 0;

    void Awake()
    {
        player.gameObject.SetActive(true);
        bgMat = transform.Find("bg").GetComponent<SpriteRenderer>().material;
    }

    void Update ()
    {
        var pos = Vector3.Lerp(transform.position, player.transform.position, Time.deltaTime);
        pos.z = transform.position.z;
        transform.position = pos;

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

    public void Add()
    {
        count++;
        if(count > minCount)
        {
            hasFinded = true;
        }
    }
}
