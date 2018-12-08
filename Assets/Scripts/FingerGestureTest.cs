using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class FingerGestureTest : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	//	FingerGestures.OnFingerEvent += OnFingerEvent;
		FingerGestures.OnGestureEvent += OnGestureHandler;
	}

	private void OnDestroy()
	{
		FingerGestures.OnGestureEvent -= OnGestureHandler;
	}

	void OnFingerEvent<T>(T eventData)
	{
		
	}

	void OnGestureHandler( Gesture gesture )
	{
		Debug.Log( "On Getsture Event:"+gesture.ToString()  +"  \r\n"+ gesture.State );
		
	}

	string List2String<T>(IList<T> values) 
	{
		StringBuilder builder = new StringBuilder();
		for(int i = 0;i<values.Count;i++)
		{
			builder.Append( values[i].ToString() );
			if( i < values.Count - 1 )
			{
				builder.Append( "," );
			}
		}
		return builder.ToString();
	}

	// Update is called once per frame
	void Update () {
		
	}
	
	
}
