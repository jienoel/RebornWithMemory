using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{

	public EventBus eventBus;
	// Use this for initialization
	void Start () {
		eventBus = new EventBus();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
