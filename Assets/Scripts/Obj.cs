using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    public virtual void Awake()
    {
        var shdowPrefab = Resources.Load<GameObject>("shadow");
        var go = GameObject.Instantiate(shdowPrefab, transform);
        go.name = "shadow";
    }
}
