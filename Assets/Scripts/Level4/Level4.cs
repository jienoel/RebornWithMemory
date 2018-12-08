﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4 : Level2
{
	public int visible;
	public Camera camera;
	public override void Start()
	{
		base.Start();
		camera = GetComponent<Camera>();
		player.OnMoveReady();
		tutor.OnMoveTutorReady();

	}

	public override void MakeSureSprite()
	{
		base.MakeSureSprite();
		distance = maxDisX;
	}

	public override void OnLevelFinished( bool success )
	{
		isLevelFinished = true;
		if(success )
		{
			tutor.uiCanvas.gameObject.SetActive( false );
			GetComponent<PinchRecognizer>().UseSendMessage = true;
		}
	}

	public override void Update()
	{
		base.Update();
		if( isLevelFinished )
		{
			if(visible == 0 && Mathf.Abs( camera.orthographicSize  - 11 ) >= 1  )
			{
				visible = 1;
				LoadNextLevel();
			}
			
		}
	}

	
	public void LoadNextLevel()
	{
		//pinchZoom.DefaultOrthoSize = maxCameraSize;
		sceneLoader.gameObject.SetActive( true );
	}

	public override float GetBlendValue( float currDis )
	{
		return Mathf.Clamp01( (distance - currDis) / distance );
	}

	public override void CheckLevelFinish()
	{
		MakeSureSprite();
		currDis = Mathf.Max( tutorSide.bounds.min.x - playerSide.bounds.max.x, 0 );
		bgRender.sharedMaterial.SetFloat( "_Blend", GetBlendValue(currDis) );
		if(  currDis > maxDisX)
		{
			Debug.Log( "Level 2 Finish:" + (currDis == 0 ? " success" : "fail"));
			OnLevelFinished(true);
		}
	}
}