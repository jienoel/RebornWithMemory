using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CircleLightType
{
	First,
	Second,
}

public class CircleLight : MonoBehaviour
{

	public CircleLightType lightType;

	private void OnCollisionEnter2D( Collision2D other )
	{
		CircleLight otherLight = other.gameObject.GetComponent<CircleLight>();
		if( otherLight != null )
		{
			if( otherLight.lightType == lightType )
			{ }
			else
			{
				
			}
		}
		
	}

}
