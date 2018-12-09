using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Level4 : Level2
{
	public int visible;
	public Camera camera;
	public Camera2DFollow camera2DFollow;
	public Transform newTargetSpotPos;
	public float delayLoadNextLevel;

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
            source.clip = successClip;
            source.loop = false;
            source.Play();

            tutor.uiCanvas.gameObject.SetActive( false );
			StartCoroutine( DelayChangeTarget() );

		}
	}

	public float delayChangeTarget = 1;
	IEnumerator DelayChangeTarget()
	{
		yield return new WaitForSeconds( 8 );
		camera2DFollow.target = newTargetSpotPos;
	}

	public override void Update()
	{
		base.Update();
		if( isLevelFinished && !camera2DFollow.isMoving)
		{
			StartCoroutine(DelayLoadNextLevel());
		}
	}

	IEnumerator DelayLoadNextLevel()
	{
		yield return new WaitForSeconds( delayLoadNextLevel );
		LoadNextLevel();
	}

	public override void OnDestroy()
	{
		base.OnDestroy();
		StopAllCoroutines();
	}

	public void LoadNextLevel()
	{
		//pinchZoom.DefaultOrthoSize = maxCameraSize;
		//sceneLoader.gameObject.SetActive( true );
		sceneLoader.GetComponent<SceneLoader>().Load( 5 );
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
