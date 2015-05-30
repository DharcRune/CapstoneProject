using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour 
{
	public int seedIndex;
	public int floorIndex;
	public int changeFloorIndex;
	public int maxFloorChanges;
	public int startingRoomType;
	public IntVector2 playerPosition;
	private int maxSeedIndex;
	private int maxRooms;
	private GameManager gm;

	void Awake() 
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	void Start() 
	{
		seedIndex = 0;
		floorIndex = 1;
		maxSeedIndex = 0;
		changeFloorIndex = 1;
		maxFloorChanges = 4;
		startingRoomType = 0;
		maxRooms = 2;
		playerPosition = new IntVector2(-4, 4);
	}

	void OnLevelWasLoaded(int level) 
	{
		if(level == 1 && maxSeedIndex == 0)
		{
			gm = GameObject.Find("GameManager").GetComponent<GameManager>();
			maxSeedIndex = gm.seeds.Count;
		}
	}

	public void StartGame()
	{
		Application.LoadLevel("MazeGeneration");
	}

	public void QuitGame()
	{
		Application.Quit();
	}

	public void savePlayerPosition(float xPosition, float zPosition)
	{
		playerPosition = new IntVector2((int)xPosition, (int)zPosition);
	}
	
	public void lowerFloorIndexByOne()
	{
		floorIndex--;
	}
	
	public void increaseFloorIndexByOne()
	{
		floorIndex++;
	}

	public void increaseChangeFloorIndexByOne()
	{
		changeFloorIndex++;
	}
	
	public void lowerSeedIndexByOne()
	{
		seedIndex--;
		if(seedIndex < 0)
		{
			seedIndex = maxSeedIndex - 1;
		}	
	}
	
	public void increaseSeedIndexByOne()
	{
		seedIndex++;
		if(seedIndex == maxSeedIndex)
		{
			seedIndex = 0;
		}	
	}

	public void lowerStartingRoomTypeByOne()
	{
		startingRoomType--;
		if(startingRoomType < 0)
		{
			startingRoomType = maxRooms - 1;
		}	
	}
	
	public void increaseStartingRoomTypeByOne()
	{
		startingRoomType++;
		if(startingRoomType == maxRooms)
		{
			startingRoomType = 0;
		}	
	}

	public void resetChangeFloorIndex()
	{
		changeFloorIndex = 0;
	}
}