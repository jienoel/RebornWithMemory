using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SocialPlatforms;

public enum FlyObjType
{
	triangle,
	star,
	pentagon,
	circle,
	cube,
}

public enum CasterType
{
	player,
	other,
	tutor
}

public class Fly : MonoBehaviour
{

	public TargetArea target;
	public FlyObjType flyObjType;
	public CasterType caterType;
	public float minY = -50;
	public float maxY = 50;
	public float t = 1;
	public float deltaDis = 0.1f;
	public float gravity = -1f;
	private Rigidbody2D _rigidbody2D;

	private bool isFly;

	private Vector2 velocity;

	private float startTime;

	private Vector3 startPos;

	private Vector3 targetPos;

	public float dropSpeed = 1;

	public bool isDrop;

	public bool isFlip;
	// Use this for initialization
	[ContextMenu("Run")]
	void DebugRun()
	{
		StartFly();
	}

 public	void StartFly()
	{
		velocity = CalculateSpeed();
		startTime = 0;
		startPos = transform.position;
		isFly = true;
	}


	public void OnHit(bool hit)
	{
		if( hit )
			Destroy( gameObject );
		else
		{
			isDrop = true;
		}
	}


	void FlyToTarget()
	{
		
		Vector2 velocity = CalculateSpeed();
		Vector3 force = CalculateForce( velocity );
		//_rigidbody2D.AddForce( force );
		_rigidbody2D = gameObject.AddComponent<Rigidbody2D>();
		_rigidbody2D.velocity = velocity;
	}

	Vector3 CalculateForce(Vector2 velocity)
	{
		Vector3 force = Vector3.zero;
		return force;
	}

	Vector2 CalculateSpeed()
	{
		Vector2 speed = Vector2.zero;
		targetPos = target.GetTargetPos();
		Vector3 relativePos = transform.position - targetPos;
		float sx = Mathf.Abs( relativePos.x );
		float sy = Mathf.Abs( relativePos.y );
		speed.x = sx / t;
		gravity = (sy * 2.0f) / (t*t);
		speed.y =   gravity * t;
		if( isFlip )
		{
			speed.x = -speed.x;
		}
	//	Debug.Log( speed +"  "+relativePos +"   "+gravity+"  "+sy+"    "+transform.position+"   "+targetPos);
		return speed;
	}

	// Update is called once per frame
	void Update () {
		if( isFly )
		{
			startTime += Time.deltaTime;
			Vector3 pos = transform.position;
			pos.x = startPos.x+ velocity.x * startTime;
			pos.y = startPos.y+ velocity.y * startTime - 0.5f * gravity * startTime * startTime;
			transform.position = pos;
			if( Vector3.Distance( transform.position , targetPos ) <= deltaDis )
			{
				isFly = false;
			}

		}
		if( isDrop )
		{
			Vector3 pos = transform.position;
			pos.y -= Time.deltaTime * dropSpeed;
			transform.position = pos;
			if( transform.position.y <= minY || transform.position.y >= maxY )
			{
				Destroy( gameObject );
			}
		}
	}

	public void OnIconClick()
	{
		if( Level2.Instance != null )
		{
			Level2.Instance.OnFlyIconClick(flyObjType);
		}
	}
}
