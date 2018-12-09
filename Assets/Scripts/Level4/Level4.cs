using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityStandardAssets._2D;

public class Level4:Level2
{
	public int visible;
	public Camera camera;
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
		disableGenerate = false;
		if( success )
		{
            source.clip = successClip;
            source.loop = false;
            source.Play();
			tutor.uiCanvas.gameObject.SetActive( false );
			MoveCameraToTarget();
		}
	}

	public Transform cameraTargetPos;
	public DOTweenAnimation dotweenAnim;
	private bool needMoving;
	void MoveCameraToTarget()
	{
		Vector3 localPos = camera.transform.InverseTransformPoint( cameraTargetPos.position );
		localPos.y = camera.transform.position.y;
		localPos.z = camera.transform.position.z;
		Tweener tweener = camera.transform.DOMove( localPos , dotweenAnim.duration);
		tweener.OnComplete( OnMoveCameraToTarget );
	}

	/*public override void Update()
	{
		base.Update();
		if( needMoving )
		{
			needMoving.tr
		}
	}*/

	void OnMoveCameraToTarget()
	{
		//Debug.LogError( "  on move to target "  +camera.transform.position);
		dotweenAnim.onComplete.RemoveAllListeners();
		disableGenerate = false;
		StartCoroutine(DelayLoadNextLevel());
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
