using UnityEngine;
using System.Collections.Generic;

public class Level3 : MonoBehaviour
{
    public float GestureScale = 8.0f;
    public Vector2 GestureSpacing = new Vector2( 1.25f, 1.0f );

    private bool[] states = {false, false };
    private float time_count = 0;


    public bool hasFinded = false;

    public GameObject sceneLoader;

    private Material bgMat;
    public float animTime = 1f;
    private float animTimeCount = 0f;


    PointCloudRegognizer recognizer;

    void Awake()
    {
        recognizer = GetComponent<PointCloudRegognizer>();
        bgMat = transform.Find("bg").GetComponent<SpriteRenderer>().material;
    }

    void OnCustomGesture( PointCloudGesture gesture )
    {
        if (hasFinded)
        {
            return;
        }
        if (recognizer.Templates.Contains(gesture.RecognizedTemplate))
        {
            if(gesture.RecognizedTemplate.name == "zhengfangxing" && !states[0])
            {
                Debug.Log("正方形");
                states[0] = true;
                time_count = 10;
            }
            else if (gesture.RecognizedTemplate.name == "yuan" && !states[1])
            {
                Debug.Log("圆");
                states[1] = true;
                time_count = 10;
            }
        }
    }

    void Update()
    {
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

        if (states[0] == false && states[1] == false)
        {
            //都没画
            return;
        }

        if(states[0] && states[1])
        {
            //都画好了 成功
            Debug.Log("都画好了 成功");

            states[0] = false;
            states[1] = false;
            hasFinded = true;
            return;
        }

        //画好了一个

        time_count -= Time.deltaTime;
        if (time_count <= 0)
        {
            //倒计时结束 失败
            Debug.Log("倒计时结束 失败");
            states[0] = false;
            states[1] = false;
        }
    }
}
