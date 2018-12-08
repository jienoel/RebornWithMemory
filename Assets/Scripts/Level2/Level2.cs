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

	public static Level2 Instance;

	void Awake()
	{
		if( Instance==null )
			Instance = this;
	}

	// Use this for initialization
	void Start()
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
		Debug.Log( "On Fly object click:"+flyObjType );
		player.CastFlyObject( flyObjType );
	}

	void MakeSureSprite()
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

	private void Update()
	{
		if( !isLevelFinished )
			CheckLevelFinish();
	}

	public float currDis = 0;
	public virtual void CheckLevelFinish()
	{
		MakeSureSprite();
		 currDis = Mathf.Max( tutorSide.bounds.min.x - playerSide.bounds.max.x, 0 );
		bgRender.sharedMaterial.SetFloat( "_Blend",  (distance - currDis)/distance);
		if( currDis == 0)
		{
			Debug.Log( "Level 2 Success!" );
			isLevelFinished = true;
		}
		
	}


}
