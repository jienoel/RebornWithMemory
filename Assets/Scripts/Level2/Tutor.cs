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

	public float needsSpan = 2;

	public float generateInterval = 3;

	public FlyNeeds flyNeeds;
	
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
			if( !flyNeeds.isStarted)
			{
				flyNeeds.isStarted = true;
				Invoke( "GenerateNeeds", generateInterval );
			}

		}
	}
	
	

	void GenerateNeeds()
	{
		flyNeeds.OnNeedTimeOut = OnNeedTimeOut;
		flyNeeds.OnFinish = OnFinish;
		int value = Random.Range( 1, iconPoses.Length );
		List<FlyObjType> needTypes = new List<FlyObjType>();
		for( int i = 0; i < value; i++ )
		{
			needTypes.Add(  iconWant[Random.Range(1 ,iconWant.Count )].flyObjType);
		}
		flyNeeds.timeout = needsSpan;
		flyNeeds.ProcessNeeds( needTypes );
		
	}

	void OnFinish()
	{
		flyNeeds.StopNeeds();
		OnHit();
		Invoke( "GenerateNeeds", generateInterval );
	}

	void OnNeedTimeOut(int leftCount)
	{
		flyNeeds.StopNeeds();
		for( int i = 0; i < leftCount; i++ )
		{
			OnMiss();
		}
		Invoke( "GenerateNeeds", generateInterval );
	}

	void OnFlyHit(Fly fly)
	{
		//Debug.Log( "On target Fly Hit:" +fly.caterType+"   "+fly.flyObjType );
		bool hit = false;
		if( fly.caterType == CasterType.player && flyNeeds.OnFit( fly.flyObjType ))
		{
			hit = true;
			//拉近距离
			//OnHit();
		}
		else
		{
			//拉远距离
			OnMiss();
		}
		fly.OnHit(hit);
		
	}

	void OnHit()
	{
		Vector3 pos = targetStonePos;
		Vector3 tutorPos = targetPos;
		pos.x += xRightSpeed;
		pos.y += yUpSpeed;
		tutorPos.x += xRightSpeed;
		tutorPos.y += yUpSpeed;
		targetStonePos = pos;
		targetPos = tutorPos;
		Level2.Instance.player.GetFlyResponse( true );
	}

	void OnMiss()
	{
		Vector3 pos = targetStonePos;
		Vector3 tutorPos = targetPos;
		pos.x -= xRightSpeed;
		tutorPos.x -= xRightSpeed;
		targetStonePos = pos;
		targetPos = tutorPos;
		Level2.Instance.player.GetFlyResponse( false );
	}

}
