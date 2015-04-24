using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public Maze mazePrefab;
	public Player playerPrefab;
	public CameraMovement cameraPrefab;
	private Maze mazeInstance;
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
		mazeInstance.Generate();

		playerInstance = Instantiate(playerPrefab) as Player;
		playerInstance.SetLocation(mazeInstance.GetCell(playerStartingPosition));

		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0.81f, 0.67f, 0.35f, 0.35f);
		cameraPrefab.startCamera();
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
