using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour 
{
	public int seedIndex;
	public int floorIndex;
	public int changeFloorIndex;
	public int maxFloorChanges;
	private int maxSeedIndex;
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
		Application.LoadLevel(1);
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

	public void resetChangeFloorIndex()
	{
		changeFloorIndex = 0;
	}
}