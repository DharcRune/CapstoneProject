﻿using UnityEngine;
using System.Collections;

public class LoadMaze : MonoBehaviour 
{
	public float timeToWait;
	private float timer;
	// Use this for initialization
	void Start() 
	{
		timer = 0f;
	}
	
	// Update is called once per frame
	void Update()
	{
		timer += Time.deltaTime;

		if(timer >= timeToWait)
		{
			// ... attack.
			ChangeScene(1);
		}
	}

	void ChangeScene(int sceneNumber)
	{
		Application.LoadLevel(sceneNumber);
	}
}
