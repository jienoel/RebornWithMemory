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
	
	private void Start()
	{
		targetStonePos = baseStore.position;
		targetPos = transform.position;
	}

	public void CastFlyObject(FlyObjType flyType)
	{
		Fly fly = GameObject.Instantiate( flyPrefab);
		fly.gameObject.transform.parent = transform.parent;
		fly.gameObject.transform.position = flyPos.position;
		fly.flyObjType = flyType;
		fly.gameObject.SetActive( true );
		fly.caterType = casterType;
		fly.StartFly();

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

