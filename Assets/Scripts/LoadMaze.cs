using UnityEngine;
using System.Collections;

public class LoadMaze : MonoBehaviour 
{
	public float timeToWait;
	private float timer;
	private FloorManager fm;

	void Start() 
	{
		timer = 0f;
		fm = GameObject.Find("FloorManager").GetComponent<FloorManager>();
	}

	void Update()
	{
		timer += Time.deltaTime;

		if(timer >= timeToWait)
		{
			Debug.Log("Changed: " + fm.changeFloorIndex + " times.");

			if(fm.changeFloorIndex > fm.maxFloorChanges)
			{
				fm.resetChangeFloorIndex();
			}

			Application.LoadLevel("MazeGeneration");
		}
	}
}