using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNeeds : MonoBehaviour
{

	public float timeout;
	public float showInterval;

	public List<Fly> needsType;

	public Action<int,int> OnNeedTimeOut;
	public Action<int> OnFinish;
	public Action OnSpawnFinished;
	public bool isFinished;

	public bool isStarted;


	public void ProcessNeeds(List<FlyObjType> needs, int flyIndex)
	{
		StopAllCoroutines();
		hasHitByOther = false;
		this.flyIndex = flyIndex;
		isFinished = false;
		isStarted = true;
		needsType = new List<Fly>();
		int i = 0;
		StartCoroutine( GenerateFly( needs, i , showInterval) );
		
		
	}

	IEnumerator GenerateFly(List<FlyObjType> needs, int i , float interval )
	{
		yield return new WaitForSeconds( interval );
		FlyObjType objType = needs[i];
		Fly flyObj = GameObject.Instantiate( Level2.Instance.tutor.iconWant.Find( ( fly ) => { return fly.flyObjType == objType; } ) );
		flyObj.transform.position = Level2.Instance.tutor.iconPoses[i].position;
		flyObj.transform.localScale = Level2.Instance.tutor.iconPoses[i].localScale;
		flyObj.gameObject.SetActive( true );
		flyObj.transform.parent = transform;
		needsType.Add( flyObj );
	//	Debug.Log(  Time.realtimeSinceStartup + "   " +"---------> start generate :"+flyObj.flyObjType +"   "+ needs[i] );
		i++;
		if( i < needs.Count )
			StartCoroutine( GenerateFly( needs, i, showInterval ) );
		else
		{
			if( OnSpawnFinished != null )
				OnSpawnFinished();
			StartCoroutine( OnTimeOut ());
		}
	}

	public int flyIndex;

	public bool hasHitByOther;
	// Update is called once per frame
	void Update () {
		
	}

	public bool OnFit(FlyObjType flyObjType, bool isOther)
	{
		
		if( needsType.Count == 0 )
		{
		//	Debug.LogError( Time.realtimeSinceStartup + "   " + "Miss 0 " + flyObjType);
			return false;
		}
	

		Fly find = needsType[0]; 
		if( find.flyObjType == flyObjType )
		{
		//	Debug.Log( Time.realtimeSinceStartup + "   " + "========================>hit "+ flyObjType);
			needsType.RemoveAt( 0 );
			GameObject findObject = find.gameObject;
			find.gameObject.SetActive( false );
			Destroy( find );
			Destroy( findObject );
			hasHitByOther |= isOther;
			if( needsType.Count == 0 )
			{
				isFinished = true;
				if( OnFinish != null )
				{
					OnFinish(flyIndex);
				}
			}
			return true;
		}
		//Debug.LogError( Time.realtimeSinceStartup + "   " + "miss:"+ find.flyObjType+"   provide:"+ flyObjType );
		return false;
	}


	public void StopNeeds()
	{
		StopAllCoroutines();
		OnFinish = null;
		OnNeedTimeOut = null;
		OnSpawnFinished = null;
		foreach( Fly find in needsType )
		{
			
		//	Debug.LogError( Time.realtimeSinceStartup + "   " + "Destroy "+find.flyObjType );
			GameObject findObject = find.gameObject;
			find.gameObject.SetActive( false );
			Destroy( find );
			Destroy( findObject );
		}
		needsType.Clear();
	}

	IEnumerator OnTimeOut()
	{
		yield return new WaitForSeconds( timeout );
		isFinished = true;
		if( OnNeedTimeOut != null )
		{
			OnNeedTimeOut(needsType.Count, flyIndex);
		}
	}
}
