using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    void Awake()
    {
        var shdowPrefab = Resources.Load<GameObject>("shadow");
        GameObject.Instantiate(shdowPrefab, transform);
    }
}
