using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutor : MonoBehaviour
{

	public TargetArea targetArea;
	public Transform baseStone;
    public Transform[] iconPoses;
	public int iconSpawnLimitX;
	public int iconSpawnLimitY;
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

	public Canvas uiCanvas;
	public int flyIndex;

	public int lockFlyIndex;

	public Player autoSpawnPlayer;

	public bool needAutoSpawn;
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
			if(Vector3.Distance( baseStone.position, targetStonePos ) > deltaPos )
				baseStone.position = Vector3.Lerp( baseStone.position, targetStonePos, Time.deltaTime * speed );
			/*if( Vector3.Distance( transform.position, targetPos ) > deltaPos )
				transform.position = Vector3.Lerp( transform.position, targetPos, Time.deltaTime * speed );*/

		}
	}

	public void OnMoveTutorReady()
	{
		if(flyNeeds.isStarted)
			return;
		flyNeeds.isStarted = true;
		uiCanvas.gameObject.SetActive( true );
		Invoke( "GenerateNeeds", generateInterval );
		Level2.Instance.OnMoveTutorReady();
	}

	void GenerateNeeds()
	{
		if( !Level2.Instance.isLevelFinished )
		{
			if( lockFlyIndex != -1 )
			{
				Debug.LogError( Time.realtimeSinceStartup + "   " + "当前处于lock 状态，可是却继续生成 "+lockFlyIndex );
				return;
			}
			flyNeeds.OnNeedTimeOut = OnNeedTimeOut;
			flyNeeds.OnFinish = OnFinish;
			
			int value = Random.Range( Mathf.Min(iconSpawnLimitX, iconPoses.Length-1  ),  Mathf.Min(iconSpawnLimitY, iconPoses.Length )  );
			List<FlyObjType> needTypes = new List<FlyObjType>();
			for( int i = 0; i < value; i++ )
			{
				needTypes.Add(  iconWant[Random.Range(1 ,iconWant.Count )].flyObjType);
			}
			flyNeeds.timeout = needsSpan;
			flyIndex++;
			lockFlyIndex = flyIndex;
			//Debug.Log( Time.realtimeSinceStartup + "   " + "0000000----------------->generate" +flyIndex );
			flyNeeds.OnSpawnFinished = () =>
			{
				if(needAutoSpawn && autoSpawnPlayer != null )
				{
					autoSpawnPlayer.AutoSpawn( needTypes );
				}
			};
			flyNeeds.ProcessNeeds( needTypes , flyIndex);
		}
		
	}

	void OnFinish(int index)
	{
		//Debug.Log( Time.realtimeSinceStartup + "   " + "----------------->Onfinish" +index );
		if( index != flyIndex )
		{
			Debug.LogError( "index != flyIndex" );
		}
		if( index == lockFlyIndex )
		{
			lockFlyIndex = -1;
		}
		bool hasHitByOther = flyNeeds.hasHitByOther;
		flyNeeds.StopNeeds();
		if(hasHitByOther )
		{
			OnMiss();
		}
		else
			OnHit();
		Invoke( "GenerateNeeds", generateInterval );
	}

	void OnNeedTimeOut(int leftCount,int index)
	{
		//Debug.Log( Time.realtimeSinceStartup + "   " + "11111111111----------------->Timeout   " +index );
		if( index != flyIndex )
		{
			Debug.LogError( "index != flyIndex" );
		}
		if( index == lockFlyIndex )
		{
			lockFlyIndex = -1;
		}
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
		if( fly.caterType == CasterType.player && flyNeeds.OnFit( fly.flyObjType, false ))
		{
			hit = true;
			//拉近距离
			//OnHit();
		}
		else if(fly.caterType == CasterType.player)
		{
			//拉远距离
			OnMiss();
		}
		else
		{
			hit = flyNeeds.OnFit( fly.flyObjType , true );
			if( hit )
			{
				targetStonePos.y += yUpSpeed;
			}
		}
		fly.OnHit(hit);
		
	}

	void OnHit()
	{
		if( Level2.Instance.currDis > 0f )
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
