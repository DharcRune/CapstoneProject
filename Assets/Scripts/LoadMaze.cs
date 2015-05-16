using UnityEngine;
using System.Collections;

public class LoadMaze : MonoBehaviour 
{
	public float timeToWait;
	private float timer;

	void Start() 
	{
		timer = 0f;
	}

	void Update()
	{
		timer += Time.deltaTime;

		if(timer >= timeToWait)
		{
			ChangeScene(1);
		}
	}

	void ChangeScene(int sceneNumber)
	{
		Application.LoadLevel(sceneNumber);
	}
}