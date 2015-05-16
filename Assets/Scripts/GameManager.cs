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
	public CurrentFloor currFloor;
	private Maze mazeInstance;
	private Player playerInstance;
	private IntVector2 playerStartingPosition;
	private float sec;
	private int seedIndex;
	private int floorIndex;
	private FloorManager fm;

	void Awake()
	{
		fm = GameObject.Find("FloorManager").GetComponent<FloorManager>();
		floorIndex = fm.floorIndex;
		seedIndex = fm.seedIndex;
	}

	void Start()
	{
		playerStartingPosition = fm.playerPosition;
		currFloor.setFloor (floorIndex);

		mazeInstance = Instantiate(mazePrefab) as Maze;
		mazeInstance.name = "maze" + floorIndex + "f";
		playerInstance = Instantiate(playerPrefab) as Player;

		Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = false;

		BeginGame();
	}

	private void BeginGame()
	{	
		mazeInstance.Generate(0f, seeds[seedIndex], mazeInstance.name);
		Debug.Log(mazeInstance.name + "'s seed: " + seeds[seedIndex]);

		//playerInstance.SetLocation(mazeInstance.GetCell(playerStartingPosition));
		playerInstance.SetLocation (playerStartingPosition);

		Camera.main.clearFlags = CameraClearFlags.Depth;
		Camera.main.rect = new Rect(0.81f, 0.67f, 0.35f, 0.35f);
		cameraPrefab.startCamera();
	}
}