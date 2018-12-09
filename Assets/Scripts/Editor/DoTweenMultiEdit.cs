using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Core;
using UnityEngine;
using  UnityEditor;

public class DoTweenMultiEdit : EditorWindow {

	[MenuItem("Tools/DoTween")]
	static void Init()
	{
		EditorWindow.GetWindow<DoTweenMultiEdit>().Show();
	}

	void OnGUI()
	{
		GUILayout.BeginVertical(  );
		animationType =(DOTweenAnimationType) EditorGUILayout.EnumPopup( "DOTweenAnimationType", animationType ) ;
		delayTime = EditorGUILayout.FloatField( "delay:", delayTime );
		duration = EditorGUILayout.FloatField( "duration:", duration );
		endValueFade = EditorGUILayout.FloatField( "endValueFade:", endValueFade );
		if( GUILayout.Button( "AddDotweenToAllFade" ) )
		{
			AddDotweenToAllFade();
		}
		if( GUILayout.Button( "AddDotweenEffect" ) )
		{
			AddDotweenEffect();
		}

		if( GUILayout.Button( "Enable ALl" ) )
		{
			EnableAll( true );
		}

		if( GUILayout.Button( " Disable All" ) )
		{
			EnableAll( false );
		}
		GUILayout.EndVertical();
	}

	void AddDotweenToAllFade()
	{
		GameObject[] objects = Selection.gameObjects;
		foreach( GameObject obj in objects )
		{
			DOTweenAnimation doTweenAnimation = obj.GetComponent<DOTweenAnimation>();
			if( doTweenAnimation == null )
			{
				doTweenAnimation = obj.AddComponent<DOTweenAnimation>();
			}
		}
	}

	public DOTweenAnimationType animationType;
	public float delayTime;
	public float duration;
	public float endValueFade;
	void AddDotweenEffect()
	{
		GameObject[] objects = Selection.gameObjects;
		foreach( GameObject obj in objects )
		{
			SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
			if(spriteRenderer == null)
				continue;
			DOTweenAnimation doTweenAnimation = obj.GetComponent<DOTweenAnimation>();
			if( doTweenAnimation == null )
			{
				doTweenAnimation = obj.AddComponent<DOTweenAnimation>();
			}
			doTweenAnimation.animationType = DOTweenAnimationType.Fade;
			doTweenAnimation.delay = delayTime;
			Color color = spriteRenderer.color;
			doTweenAnimation.duration = duration;
			doTweenAnimation.endValueFloat = endValueFade;
			color.a = 0;
			spriteRenderer.color = color;
		}
	}

	void EnableAll(bool enable)
	{
		GameObject[] objects = Selection.gameObjects;
		foreach( GameObject obj in objects )
		{
			SpriteRenderer spriteRenderer = obj.GetComponent<SpriteRenderer>();
			if(spriteRenderer == null)
				continue;
			DOTweenAnimation doTweenAnimation = obj.GetComponent<DOTweenAnimation>();
			if( doTweenAnimation == null )
			{
				doTweenAnimation = obj.AddComponent<DOTweenAnimation>();
			}
			doTweenAnimation.animationType = DOTweenAnimationType.Fade;
			doTweenAnimation.delay = delayTime;
			Color color = spriteRenderer.color;
			doTweenAnimation.duration = duration;
			doTweenAnimation.endValueFloat = endValueFade;
			color.a = 1;
			spriteRenderer.color = color;
			obj.SetActive( enable );
		}
	}
}
