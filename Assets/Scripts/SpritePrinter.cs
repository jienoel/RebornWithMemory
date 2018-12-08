using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpritePrinter : MonoBehaviour {

#if UNITY_EDITOR
	[ContextMenu("Print Sprite Size")]
	void PrintSpriteSize()
	{
		SpriteRenderer render = Selection.activeGameObject.GetComponent<SpriteRenderer>();
		if( render != null )
		{
			Debug.Log( render.gameObject.name+"  "+render.bounds.center+"  "+render.bounds.extents+"  "+render.bounds.min+"   "+render.bounds.max );
		}
	}
#endif
}
