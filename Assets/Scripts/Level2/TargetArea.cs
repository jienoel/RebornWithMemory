using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
[RequireComponent(typeof(SpriteRenderer))]
public class TargetArea : MonoBehaviour
{

	public SpriteRenderer render;
	public Action<Fly> onFlyHit;
	
	void Start()
	{
		render = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D( Collider2D other )
	{
		
		Fly fly = other.transform.parent.GetComponent<Fly>();
		Debug.Log( "On Trigger Enter  " + (fly == null).ToString()+"    "+(onFlyHit == null).ToString() );
		if( fly != null  && onFlyHit!=null)
		{
			onFlyHit( fly );
		}
	}
	

	public Vector3 GetTargetPos()
	{
		Vector3 pos = render.bounds.center;
		Random random = new Random();
		
		pos.x = random.Next((int)render.bounds.min.x, (int)render.bounds.max.x);
		pos.y = random.Next( (int)render.bounds.min.y, (int)render.bounds.max.y );
		return render.bounds.center;
		return pos;
	}
}
