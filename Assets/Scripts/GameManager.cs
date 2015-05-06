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
	private Maze mazeInstance2;
	private Player playerInstance;
	private IntVector2 playerStartingPosition;
	private float sec;
	private int seedIndex;

	void Start()
	{
		playerStartingPosition = new IntVector2(0, mazePrefab.size.x - 1);
		seedIndex = 0;

		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.name = "maze1f";
		mazeInstance2 = Instantiate(mazePrefab) as Maze;
		mazeInstance2.name = "maze2f";
		playerInstance = Instantiate(playerPrefab) as Player;

		BeginGame();
	}

	void Update()
	{	
		InvokeRepeating("RestartGame", 10.0f, 10.0f);
	}

	private void BeginGame()
	{
		if(seedIndex == seeds.Count - 1)
		{
			seedIndex = 0;
		}

		mazeInstance.Generate(0f, seeds[seedIndex], mazeInstance.name);
		mazeInstance2.Generate(10f, seeds[seedIndex + 1], mazeInstance2.name);
		
		playerInstance.SetLocation(mazeInstance.GetCell(playerStartingPosition));

		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0.81f, 0.67f, 0.35f, 0.35f);
		cameraPrefab.startCamera();
	}

	private void RestartGame()
	{
		Debug.Log("Its been 10 seconds!");
		CancelInvoke();

		seedIndex++;

		Destroy (mazeInstance.gameObject);
		Destroy (mazeInstance2.gameObject);
		Destroy (playerInstance.gameObject);

		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.name = "maze1f";
		mazeInstance2 = Instantiate(mazePrefab) as Maze;
		mazeInstance2.name = "maze2f";
		playerInstance = Instantiate(playerPrefab) as Player;

		BeginGame();
	}
}
