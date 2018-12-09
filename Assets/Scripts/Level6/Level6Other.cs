using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Level6Other : Obj
{
    public Level6 mgr;
    private SpriteRenderer sp;
    private bool isOn = false;
    private PlayerShadow shadow;
    public override void Awake()
    {
        base.Awake();
        sp = GetComponent<SpriteRenderer>();
        shadow = transform.Find("shadow").GetComponent<PlayerShadow>();
        shadow.color.a = 0;
    }

	void Update ()
    {
        if (mgr.hasFinded)
        {
            return;
        }
        if (!isOn)
        {
            if(Vector3.Distance(transform.position, mgr.player.transform.position)< mgr.zoom)
            {
                isOn = true;
                mgr.Add();
                var timeCount = 0f;
                var start = 0;
                var target = 68f / 255f;
                sp.DOFade(1, 0.4f).OnUpdate(()=> {
                    timeCount += Time.deltaTime;
                    shadow.color.a = Mathf.Lerp(start, target, timeCount / 0.4f);
                }).OnComplete(()=>
                {
                    shadow.color.a = target;
                });
            }
        }
	}
}
