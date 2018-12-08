using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2:MonoBehaviour
{


	public Tutor tutor;
	public Player player;
	public List<Fly> flyPrefabs;
	public SpriteRenderer playerSide;
	public SpriteRenderer tutorSide;
	public SpriteRenderer bgRender;
	public float distance = 10;
	public Action LevelFinished;
	public bool isLevelFinished;
	Dictionary<FlyObjType, Fly> flyPreDic = new Dictionary<FlyObjType, Fly>();
	public bool isPlayerReady;
	public bool isTutorReady;
	public static Level2 Instance;
	public float maxDisX;
	public float initBlend;
	public Canvas sceneLoader;
	
public virtual	void Awake()
	{
		if( Instance==null )
			Instance = this;
        bgRender.sharedMaterial.SetFloat("_Blend", initBlend);
		
	}
	

	// Use this for initialization
	public virtual		void Start()
	{
		foreach( Fly fly in flyPrefabs )
		{
			try
			{
				flyPreDic.Add( fly.flyObjType, fly );
			}
			catch( Exception e )
			{
				Debug.LogException( e );
			}
		}

	}


	public void OnFlyIconClick(FlyObjType flyObjType)
	{
//		Debug.Log( "On Fly object click:"+flyObjType );
		player.CastFlyObject( flyObjType );
	}

	public  virtual void MakeSureSprite()
	{
		bool needSetBlendValue = false;
		if( playerSide == null )
		{
			playerSide = player.baseStore.GetComponent<SpriteRenderer>();
			needSetBlendValue = true;
		}
		if( tutorSide == null )
		{
			tutorSide = tutor.baseStone.GetComponent<SpriteRenderer>();
			needSetBlendValue = true;
		}
		if( needSetBlendValue )
		{
			distance = tutorSide.bounds.min.x - playerSide.bounds.max.x;
		}
	}

	public	virtual	 void Update()
	{
		if( !isLevelFinished && isPlayerReady && isTutorReady)
			CheckLevelFinish();
	}

	public	virtual	 void OnDestroy()
	{
		Instance = null;
        bgRender.sharedMaterial.SetFloat("_Blend", initBlend);
    }

	public float currDis = 0;

	public void OnMovePlayerReady()
	{
		isPlayerReady = true;
		tutor.gameObject.SetActive( true );
	}

	public void OnMoveTutorReady()
	{
		isTutorReady = true;
	}

	public virtual void CheckLevelFinish()
	{
		MakeSureSprite();
		 currDis = Mathf.Max( tutorSide.bounds.min.x - playerSide.bounds.max.x, 0 );
		bgRender.sharedMaterial.SetFloat( "_Blend", GetBlendValue(currDis) );
		if( currDis == 0 || currDis > maxDisX)
		{
			Debug.Log( "Level 2 Finish:" + (currDis == 0 ? " success" : "fail"));
			OnLevelFinished(currDis == 0);
		}
		
	}

	public virtual void OnLevelFinished(bool success)
	{
		isLevelFinished = true;
		if( success )
		{
			tutor.uiCanvas.gameObject.SetActive( false );
			sceneLoader.gameObject.SetActive( true );
		}
	}

	public virtual float GetBlendValue(float currDis)
	{
		return Mathf.Clamp01( (distance - currDis) / distance );
	}


}
