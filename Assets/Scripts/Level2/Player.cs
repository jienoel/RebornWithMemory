using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{

	public CasterType casterType = CasterType.player;
	public Fly flyPrefab;
	public Transform flyPos;
	public Transform baseStore;
	public float yUpSpeed = 0.2f;
	public float xRightSpeed = 0.2f;
	public Vector3 targetStonePos;
	public Vector3 targetPos;
	public float deltaPos = 0.1f;
	public float speed = 1;
	public Transform[] iconPoses;
	public List<Fly> iconWant;
	public int showIndex;
	public float flyDelay = 0.1f;
	public List<Fly> needFlyList;
	public Fly[] flyingList;
	private void Start()
	{
		targetStonePos = baseStore.position;
		targetPos = transform.position;
		needFlyList = new List<Fly>();
	}

	public void PopFlyObject( FlyObjType flyType )
	{
		Fly flyPrefab = iconWant.Find( ( flyTypeIn ) => { return flyTypeIn.flyObjType == flyType; } );
		if( flyPrefab != null )
		{
			Fly fly = GameObject.Instantiate( flyPrefab );
			Transform trans = iconPoses[showIndex % 3];
			fly.transform.position = trans.position;
			fly.transform.localScale = trans.localScale;
			fly.gameObject.SetActive( true );
			needFlyList.Add( fly );
			showIndex++;
			//fly.dropSpeed = -fly.dropSpeed;
			if( showIndex % 3 == 0 )
			{
				int index = 0;
				StartCoroutine( Flying(index , needFlyList.ToArray() ));
				needFlyList.Clear();
			}
		}
		else
		{
			Debug.LogError( "fly prefab is null! " );
		}
	}

	public void OnTutorNeedsTimeout()
	{
		foreach( var fly in flyingList )
		{
			fly.OnHit( false );
		}
		foreach( var fly in needFlyList )
		{
			fly.OnHit( false );
		}
	}

	IEnumerator Flying(int i, Fly[] flies)
	{
		yield return new WaitForSeconds( flyDelay );
		Fly fly = flies[i];
		Level2.Instance.tutor.OnFlyHit( fly );
		i++;
		
		if( i < 3 )
		{
			StartCoroutine( Flying( i, flies ) );
		}

	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}

	public void CastFlyObject(FlyObjType flyType)
	{
		if( casterType == CasterType.player )
		{
			PopFlyObject( flyType );
		}
		else
		{
			Fly fly = GameObject.Instantiate( flyPrefab);
			fly.gameObject.transform.parent = transform.parent;
			fly.gameObject.transform.position = flyPos.position;
			fly.flyObjType = flyType;
			fly.gameObject.SetActive( true );
			fly.caterType = casterType;
			fly.StartFly();
		}
	}

	public void GetFlyResponse( bool hit )
	{
		if( hit )
		{
			if( Level2.Instance.currDis > 0f )
			{
				targetStonePos.x += xRightSpeed;
				targetStonePos.y += yUpSpeed;
				targetPos.y += yUpSpeed;
				targetPos.x += xRightSpeed;
			}
			
		}
		else
		{
			targetStonePos.x -= xRightSpeed;
			targetStonePos.y -= yUpSpeed;
			targetPos.y -= yUpSpeed;
			targetPos.x -= xRightSpeed;
		}

	}

	private void Update()
	{
		if( !Level2.Instance.isLevelFinished )
		{
			if(Vector3.Distance( baseStore.position, targetStonePos ) > deltaPos )
			{
				baseStore.position = Vector3.Lerp( baseStore.position, targetStonePos, Time.deltaTime*speed );
			}
			/*if( Vector3.Distance( transform.position, targetPos ) > deltaPos )
			{
				transform.position = Vector3.Lerp( transform.position, targetPos, Time.deltaTime * speed );
			}*/
		}
	}

	public void OnMoveReady()
	{
		Level2.Instance.OnMovePlayerReady();
	}

	public void AutoSpawn(List<FlyObjType> flyObjTypes)
	{
		int i = 0;
		StartCoroutine( AutoSpawnOnByOne( i, flyObjTypes ) );
	}

	IEnumerator AutoSpawnOnByOne(int i, List<FlyObjType> flyObjTypes)
	{
		yield return new WaitForSeconds( 0.3f );
		CastFlyObject( flyObjTypes[i++] );
		if( i < flyObjTypes.Count )
		{
			StartCoroutine( AutoSpawnOnByOne( i, flyObjTypes ) );
		}
	}

	
}

