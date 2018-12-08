using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNeeds : MonoBehaviour
{

	public float timeout;

	public List<Fly> needsType;

	public Action<int> OnNeedTimeOut;
	public Action OnFinish;
	public bool isFinished;

	public bool isStarted;


	public void ProcessNeeds(List<FlyObjType> needs)
	{
		isFinished = false;
		isStarted = true;
		needsType = new List<Fly>();
		for( int i = 0; i < needs.Count; i++ )
		{
			FlyObjType objType = needs[i];
			Fly flyObj = GameObject.Instantiate( Level2.Instance.tutor.iconWant.Find( ( fly ) => { return fly.flyObjType == objType; } ) );
			flyObj.transform.position = Level2.Instance.tutor.iconPoses[i].position;
			flyObj.transform.localScale = Level2.Instance.tutor.iconPoses[i].localScale;
			flyObj.gameObject.SetActive( true );
			needsType.Add( flyObj );
		}
		Invoke( "OnTimeOut", timeout );
	}

	// Update is called once per frame
	void Update () {
		
	}

	public bool OnFit(FlyObjType flyObjType)
	{
		Fly find = needsType.FindLast( ( fly ) => { return fly.flyObjType == flyObjType; } );
		if( find != null )
		{
			Destroy( find.gameObject );
			needsType.Remove( find );
			if( needsType.Count == 0 )
			{
				isFinished = true;
				if( OnFinish != null )
				{
					OnFinish();
				}
			}
			return true;
		}
		return false;
	}


	public void StopNeeds()
	{
		StopAllCoroutines();
		OnFinish = null;
		OnNeedTimeOut = null;
		foreach( var need in needsType )
		{
			Destroy( need.gameObject );
		}
	}

	void OnTimeOut()
	{
		isFinished = true;
		if( OnNeedTimeOut != null )
		{
			OnNeedTimeOut(needsType.Count);
		}
	}
}
