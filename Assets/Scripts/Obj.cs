using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obj : MonoBehaviour
{
    public virtual void Awake()
    {
        var shdowPrefab = Resources.Load<GameObject>("shadow");
        GameObject.Instantiate(shdowPrefab, transform);
    }
}
