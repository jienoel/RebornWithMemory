using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGrid : MonoBehaviour {

	[ContextMenu( "Grid" )]
	public void DoGrid()
	{
		
		SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>(false);
		if(sprites.Length ==0)
			return;
		float maxX = sprites[0].bounds.extents.x * 2;
		float maxY = sprites[0].bounds.extents.y * 2;
		for( int i = 1; i < sprites.Length; i++ )
		{
			maxX = Mathf.Max( sprites[i].bounds.extents.x * 2 );
			maxY = Mathf.Max( sprites[i].bounds.extents.y * 2 );
		}
		
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
