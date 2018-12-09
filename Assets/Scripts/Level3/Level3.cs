using UnityEngine;
using System.Collections.Generic;

public class Level3 : MonoBehaviour
{
    public float GestureScale = 8.0f;
    public Vector2 GestureSpacing = new Vector2( 1.25f, 1.0f );

    public Animation[] anims;
    private bool[] states = {false, false };
    private float time_count = 0;

    public bool hasFinded = false;

    public GameObject sceneLoader;

    PointCloudRegognizer recognizer;
    FingerUpEvent e;

    void Awake()
    {
        recognizer = GetComponent<PointCloudRegognizer>();
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

    public void OnFingerUp(FingerUpEvent e)
    {
        this.e = e;
    }

    void Update()
    {
        if (hasFinded)
        {
            return;
        }

        if(states[0] && states[1])
        {
            //都画好了 成功
            Debug.Log("都画好了 成功");

            states[0] = false;
            states[1] = false;
            anims[0].Play();
            anims[1].Play();
            hasFinded = true;
            sceneLoader.SetActive(true);
            return;
        }
        else
        {
            if (e != null)
            {
                anims[e.Position.x / Screen.width < 0.5f ? 1 : 0].Play();
                e = null;
            }
        }

        if (states[0] == false && states[1] == false)
        {
            //都没画
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
