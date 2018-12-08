using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level1 : MonoBehaviour {
    public Level1Player player;
    public Level1BastOther bast_other;
    public float zoom = 2f;
    public float closeDist = 0.1f;
    private Level1Other[] others;

    public bool hasFinded = false;
    private Material bgMat;
    public float animTime = 1f;
    private float animTimeCount = 0f;

    public GameObject sceneLoader;

    void Awake()
    {
        player.gameObject.SetActive(true);
        others = GameObject.FindObjectsOfType<Level1Other>();
        foreach (var other in others)
        {
            other.gameObject.SetActive(true);
        }
        bgMat = transform.Find("bg").GetComponent<SpriteRenderer>().material;
    }

    void Update()
    {
        var pos = Vector3.Lerp(transform.position, player.transform.position, 0.01f);
        pos.z = transform.position.z;
        transform.position = pos;

        if (hasFinded && animTimeCount > -1f)
        {
            animTimeCount += Time.deltaTime;
            animTimeCount = Mathf.Clamp(animTimeCount, 0, animTime);
            bgMat.SetFloat("_Blend", animTimeCount / animTime);

            if(animTimeCount >= animTime)
            {
                animTimeCount = -111;
                sceneLoader.SetActive(true);
            }
        }
    }

    public void Find()
    {
        if (hasFinded)
        {
            return;
        }
        hasFinded = true;
    }

}
