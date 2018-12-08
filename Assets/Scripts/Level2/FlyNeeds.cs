using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyNeeds : MonoBehaviour
{

	public float timeout;
	public float showInterval;

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
		int i = 0;
		StartCoroutine( GenerateFly( needs, i , showInterval) );
		Invoke("OnTimeOut" , timeout );
		
	}

	IEnumerator GenerateFly(List<FlyObjType> needs, int i , float interval )
	{
		yield return new WaitForSeconds( interval );
		FlyObjType objType = needs[i];
		Fly flyObj = GameObject.Instantiate( Level2.Instance.tutor.iconWant.Find( ( fly ) => { return fly.flyObjType == objType; } ) );
		flyObj.transform.position = Level2.Instance.tutor.iconPoses[i].position;
		flyObj.transform.localScale = Level2.Instance.tutor.iconPoses[i].localScale;
		flyObj.gameObject.SetActive( true );
		needsType.Add( flyObj );
		Debug.Log( "---------> start generate :"+flyObj.flyObjType +"   "+ needs[i] );
		i++;
		if( i < needs.Count )
			StartCoroutine( GenerateFly( needs, i, showInterval ) );
	}

	// Update is called once per frame
	void Update () {
		
	}

	public bool OnFit(FlyObjType flyObjType)
	{
		if( needsType.Count == 0 )
		{
			Debug.LogError( "Miss 0 " + flyObjType);
			return false;
		}
	

		Fly find = needsType[0]; 
		if( find.flyObjType == flyObjType )
		{
			Debug.Log( "========================>hit "+ flyObjType);
			needsType.RemoveAt( 0 );
			GameObject findObject = find.gameObject;
			find.gameObject.SetActive( false );
			Destroy( find );
			Destroy( findObject );
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
		Debug.LogError( "miss:"+ find.flyObjType+"   provide:"+ flyObjType );
		return false;
	}


	public void StopNeeds()
	{
		StopAllCoroutines();
		OnFinish = null;
		OnNeedTimeOut = null;
		foreach( Fly find in needsType )
		{
			
			Debug.LogError( "Destroy "+find.flyObjType );
			GameObject findObject = find.gameObject;
			find.gameObject.SetActive( false );
			Destroy( find );
			Destroy( findObject );
		}
		needsType.Clear();
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
