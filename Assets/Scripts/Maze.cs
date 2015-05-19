using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Maze : MonoBehaviour
{
	public MazeCell[] cellPrefabs;
	public IntVector2 size;
	public MazePassage passagePrefab;
	public MazeWall[] wallPrefabs;
	public MazeDoor doorPrefab;
	public MazeRoomSettings[] roomSettings;
	private Player player;
	private FloorManager fm;
	private MazeCell[,] cells;
	private List<MazeRoom> rooms = new List<MazeRoom>();
	private int seedValue;
	private float timeToWait;
	private float timer;
	private AudioSource windAudio;

	void Start()
	{
		timer = 0f;
		timeToWait = 20f;

		fm = GameObject.Find ("FloorManager").GetComponent<FloorManager>();
		player = GameObject.Find("Player(Clone)").GetComponent<Player>();
		windAudio = GetComponent<AudioSource>();
		windAudio.Play();
	}

	void Update()
	{
		timer += Time.deltaTime;

		if(timer >= timeToWait && fm.changeFloorIndex < fm.maxFloorChanges)
		{
			fm.increaseChangeFloorIndexByOne();
			fm.increaseSeedIndexByOne();
			fm.savePlayerPosition(player.transform.position.x, player.transform.position.z);

			Application.LoadLevel("LoadingNewMaze");
		}
	}

	[Range(0f, 1f)]
	public float doorProbability;

	public MazeCell GetCell(IntVector2 coordinates)
	{
		return cells[coordinates.x, coordinates.z];
	}

	public void Generate(float heightOfCells, int seedVal, string mazeFloor)
	{
		cells = new MazeCell[size.x, size.z];

		List<MazeCell> activeCells = new List<MazeCell>();
		DoFirstGenerationStep(activeCells, heightOfCells, seedVal, mazeFloor);

		while(activeCells.Count > 0)
		{
			DoNextGenerationStep(activeCells, heightOfCells, mazeFloor);
		}
	}

	private void DoFirstGenerationStep(List<MazeCell> activeCells, float heightOfCells, int seedVal, string mazeFloor)
	{
		setSeed(seedVal);
		MazeCell newCell = CreateCell(RandomCoordinates, heightOfCells, mazeFloor);
		newCell.Initialize(CreateRoom(-1));
		activeCells.Add(newCell);
	}

	private void DoNextGenerationStep(List<MazeCell> activeCells, float heightOfCells, string mazeFloor)
	{
		int currentIndex = activeCells.Count - 1;
		MazeCell currentCell = activeCells[currentIndex];

		if(currentCell.IsFullyInitialized)
		{
			activeCells.RemoveAt(currentIndex);
			return;
		}

		MazeDirection direction = currentCell.RandomUninitializedDirection;
		IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();

		if(ContainsCoordinates(coordinates))
		{
			MazeCell neighbor = GetCell(coordinates);

			if(neighbor == null)
			{
				neighbor = CreateCell(coordinates, heightOfCells, mazeFloor);
				CreatePassage(currentCell, neighbor, direction);
				activeCells.Add(neighbor);
			}
			else if(currentCell.room == neighbor.room)
			{
				CreatePassageInSameRoom(currentCell, neighbor, direction);
			}
			else
			{
				CreateWall(currentCell, neighbor, direction);
			}
		}
		else
		{
			CreateWall(currentCell, null, direction);
		}
	}

	private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;

		MazePassage passage = Instantiate(prefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);

		passage = Instantiate(prefab) as MazePassage;

		if (passage is MazeDoor) 
		{
			otherCell.Initialize(CreateRoom(cell.room.settingsIndex));
		}
		else 
		{
			otherCell.Initialize(cell.room);
		}

		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreatePassageInSameRoom(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazePassage passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(cell, otherCell, direction);

		passage = Instantiate(passagePrefab) as MazePassage;
		passage.Initialize(otherCell, cell, direction.GetOpposite());
	}

	private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		MazeWall wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length - 1)]) as MazeWall;
		wall.Initialize(cell, otherCell, direction);

		if(otherCell != null)
		{
			wall = Instantiate(wallPrefabs[Random.Range(0, wallPrefabs.Length - 1)]) as MazeWall;
			wall.Initialize(otherCell, cell, direction.GetOpposite());
		}
	}

	private MazeCell CreateCell(IntVector2 coordinates, float height, string mazeFloor)
	{
		MazeCell newCell;

		if(transform.name.Equals("maze1f")) 
		{
			if(GameObject.Find(transform.name + "/MazeCell/Stairs2") != false)
			{
				newCell = Instantiate(cellPrefabs[Random.Range(1, cellPrefabs.Length - 1)]) as MazeCell;
			}
			else
			{
				newCell = Instantiate(cellPrefabs[cellPrefabs.Length - 1]) as MazeCell;
			}
		} 
		else 
		{
			if(GameObject.Find(transform.name + "/MazeCell/Stairs2") != false)
			{
				newCell = Instantiate(cellPrefabs[Random.Range(0, cellPrefabs.Length - 1)]) as MazeCell;
			}
			else
			{
				newCell = Instantiate(cellPrefabs[cellPrefabs.Length - 1]) as MazeCell;
			}
		}


		cells[coordinates.x, coordinates.z] = newCell;
		
		newCell.coordinates = coordinates;
		newCell.name = "MazeCell";
		newCell.transform.parent = transform;
		newCell.transform.localPosition = new Vector3 (coordinates.x - size.x * 0.5f + 0.5f, height, coordinates.z - size.z * 0.5f + 0.5f);

		return newCell;
	}

	private MazeRoom CreateRoom(int indexToExclude)
	{
		MazeRoom newRoom = ScriptableObject.CreateInstance<MazeRoom>();
		newRoom.settingsIndex = Random.Range(0, roomSettings.Length);

		if(newRoom.settingsIndex == indexToExclude)
		{
			newRoom.settingsIndex = (newRoom.settingsIndex + 1) % roomSettings.Length;
		}

		newRoom.settings = roomSettings[newRoom.settingsIndex];
		rooms.Add(newRoom);

		return newRoom;
	}

	public void setSeed(int seed)
	{
		seedValue = seed;
	}

	public IntVector2 RandomCoordinates
	{
		get 
		{ 
			Random.seed = seedValue;
			int xRange = Random.Range(0, size.x);
			int zRange = Random.Range(0, size.z);
	
			return new IntVector2(xRange, zRange); 
		}
	}

	public bool ContainsCoordinates(IntVector2 coordinate)
	{
		return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
	}
}
