using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(PointCloudRegognizer))]
public class Level3 : MonoBehaviour
{
    public float GestureScale = 8.0f;
    public Vector2 GestureSpacing = new Vector2( 1.25f, 1.0f );
    public int MaxGesturesPerRaw = 2;

    private bool[] states = {false, false };
    private float time_count = 0;
    PointCloudRegognizer recognizer;

    void Awake()
    {
        recognizer = GetComponent<PointCloudRegognizer>();
    }

    void OnCustomGesture( PointCloudGesture gesture )
    {
        if (recognizer.Templates.Contains(gesture.RecognizedTemplate))
        {
            gui_text = gesture.RecognizedTemplate.name;
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
        if(states[0] == false && states[1] == false)
        {
            //都没画
            return;
        }

        if(states[0] && states[1])
        {
            //都画好了 成功
            gui_text = "都画好了 成功";
            Debug.Log("都画好了 成功");

            states[0] = false;
            states[1] = false;
            return;
        }

        //画好了一个

        time_count -= Time.deltaTime;
        if (time_count <= 0)
        {
            //倒计时结束 失败
            gui_text = "倒计时结束 失败";
            Debug.Log("倒计时结束 失败");
            states[0] = false;
            states[1] = false;
        }

    }

    string gui_text = "";
    void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 100, 50), gui_text);
    }

    //[UnityEditor.MenuItem("Test/Test")]
    //public static void Test()
    //{
    //    string formater = "  - {{x: {0}, y: {1}}}\n";
    //    string temp = "";
    //    for (int i = 0; i < 360; i+= 10)
    //    {
    //        temp += string.Format(formater, Mathf.Cos(i * Mathf.Deg2Rad) * 0.5f, Mathf.Sin(i * Mathf.Deg2Rad) * 0.5f);
    //    }
    //    Debug.Log(temp);
    //}
}
