using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class SceneLoader: MonoBehaviour
{
    private static GameObject sceneInOut;
    private bool isLoading = false;
    public void Load(int id)
    {
        if (isLoading)
        {
            return;
        }
        if (sceneInOut == null)
        {
            sceneInOut = GameObject.Instantiate(Resources.Load<GameObject>("SceneInOut"));
            GameObject.DontDestroyOnLoad(sceneInOut);
        }
        isLoading = true;
        sceneInOut.SetActive(true);
        var img = sceneInOut.transform.Find("11111bg").GetComponent<Image>();
        var color = img.color;
        color.a = 0;
        img.color = color;
        img.DOFade(1, 1f).OnComplete(()=> {
            if (id == 1 || id == 3)
            {
                var audioSource = GameObject.Find("Audio Source");
                audioSource.transform.SetParent(null);
                DontDestroyOnLoad(audioSource);
            }
            SceneManager.LoadScene(id);
            if (id == 2 || id == 4)
            {
                var audioSource = GameObject.Find("Audio Source");
                Destroy(audioSource);
            }
            img.DOFade(0, 1f).OnComplete(()=>
            {
                sceneInOut.SetActive(false);
            });
        });
    }

}
