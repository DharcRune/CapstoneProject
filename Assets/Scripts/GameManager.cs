using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public Maze mazePrefab;
	public Player playerPrefab;
	public CameraMovement cameraPrefab;
	public List<int> seeds;
	public Timer timer;
	private Maze mazeInstance;
	private Player playerInstance;
	private IntVector2 playerStartingPosition;
	private float sec;
	private int seedIndex;
	private int floorIndex;

	void Start()
	{
		playerStartingPosition = new IntVector2(0, mazePrefab.size.x - 1);
		seedIndex = 0;
		floorIndex = 1;

		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.name = "maze" + floorIndex + "f";
		playerInstance = Instantiate(playerPrefab) as Player;

		BeginGame();
	}

	void Update()
	{	
		InvokeRepeating("RestartGame", 60.0f, 10.0f);
	}

	private void BeginGame()
	{	
		mazeInstance.Generate(0f, seeds[seedIndex], mazeInstance.name);
		Debug.Log(mazeInstance.name + "'s seed: " + seedIndex);

		seedIndex++;
		if(seedIndex == seeds.Count)
		{
			seedIndex = 0;
		}		

		playerInstance.SetLocation(mazeInstance.GetCell(playerStartingPosition));

		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0.81f, 0.67f, 0.35f, 0.35f);
		cameraPrefab.startCamera();
	}

	private void RestartGame()
	{
		Debug.Log("Its been 10 seconds!");
		CancelInvoke();

		//seedIndex++;

		Destroy (mazeInstance.gameObject);
		Destroy (playerInstance.gameObject);

		floorIndex++;
		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.name = "maze" + floorIndex + "f";

		playerInstance = Instantiate(playerPrefab) as Player;

		BeginGame();
	}
}
