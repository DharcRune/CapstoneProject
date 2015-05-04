using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
	public Maze mazePrefab;
	public Player playerPrefab;
	public CameraMovement cameraPrefab;
	public HudArrow arrowPrefab;
	public List<int> seeds;
	private Maze mazeInstance;
	private Maze mazeInstance2;
	private Player playerInstance;
	private IntVector2 playerStartingPosition;

	void Start()
	{
		playerStartingPosition = new IntVector2(0, mazePrefab.size.x - 1);
		BeginGame();
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			RestartGame();
		}
	}

	private void BeginGame()
	{
		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.name = "maze1f";
		mazeInstance2 = Instantiate(mazePrefab) as Maze;
		mazeInstance2.name = "maze2f";

		mazeInstance.Generate(0f, seeds[0], mazeInstance.name);
		mazeInstance2.Generate(10f, seeds[1], mazeInstance2.name);

		playerInstance = Instantiate(playerPrefab) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(playerStartingPosition));

		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0.81f, 0.67f, 0.35f, 0.35f);
		cameraPrefab.startCamera();
		arrowPrefab.startHudArrow();
	}

	private void RestartGame()
	{
		StopAllCoroutines();
		Destroy(mazeInstance.gameObject);

		if(playerInstance != null)
		{
			Destroy(playerInstance.gameObject);
		}

		BeginGame();
	}
}
