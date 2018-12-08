using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{

	public TargetArea targetArea;
	public Transform baseStone;
    public Transform[] iconPoses;
    public List<Fly> iconWant;
	public float yUpSpeed = 0.2f;
	public float xRightSpeed = 0.2f;

	public Vector3 targetStonePos;

	public Vector3 targetPos;
	public float deltaPos = 0.1f;
	public float speed = 1;
    
    // Use this for initialization
    void Start ()
	{
		targetArea.onFlyHit += OnFlyHit;
		targetPos = transform.position;
		targetStonePos = baseStone.position;
	}
	
	// Update is called once per frame
	void Update () {
		if( !Level2.Instance.isLevelFinished )
		{
			if( Vector3.Distance( baseStone.position, targetStonePos ) > deltaPos )
				baseStone.position = Vector3.Lerp( baseStone.position, targetStonePos, Time.deltaTime * speed );
			if( Vector3.Distance( transform.position, targetPos ) > deltaPos )
				transform.position = Vector3.Lerp( transform.position, targetPos, Time.deltaTime * speed );
		}
	}

	void OnFlyHit(Fly fly)
	{
		Debug.Log( "On target Fly Hit:" +fly.caterType+"   "+fly.flyObjType );
		Vector3 pos = targetStonePos;
		Vector3 tutorPos = targetPos;
		if( fly.caterType == CasterType.player )
		{
			//拉近距离
			pos.x += xRightSpeed;
			pos.y += yUpSpeed;
			tutorPos.x += xRightSpeed;
			tutorPos.y += yUpSpeed;
			Level2.Instance.player.GetFlyResponse( true );
		}
		else
		{
			//拉远距离
			pos.x -= xRightSpeed;
			tutorPos.x -= xRightSpeed;
			Level2.Instance.player.GetFlyResponse( false );
		}
		fly.OnHit();
		targetStonePos = pos;
		targetPos = tutorPos;
	}

}
