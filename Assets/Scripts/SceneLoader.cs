using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoader: MonoBehaviour
{
    private static GameObject sceneInOut;
    public void Load(int id)
    {
        if (sceneInOut == null)
        {
            sceneInOut = GameObject.Instantiate(Resources.Load<GameObject>("SceneInOut"));
            GameObject.DontDestroyOnLoad(sceneInOut);
        }
        sceneInOut.SetActive(true);
        var img = sceneInOut.transform.Find("11111bg").GetComponent<Image>();
        var color = img.color;
        color.a = 0;
        img.color = color;
        img.DOFade(1, 1f).OnComplete(()=> {
            SceneManager.LoadScene(id);
            img.DOFade(0, 1f).OnComplete(()=>
            {
                sceneInOut.SetActive(false);
            });
        });
    }

}
