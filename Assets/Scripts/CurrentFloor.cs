using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CurrentFloor : MonoBehaviour 
{
	public Text currentFloor;
	private int currentMazeFloor;

	void Update()
	{
		currentFloor.text = setText();
	}

	private string setText()
	{
		switch(currentMazeFloor % 10)
		{
		case 1:
			return currentMazeFloor + "st Floor";
		case 2:
			return currentMazeFloor + "nd Floor";
		case 3:
			return currentMazeFloor + "rd Floor";
		default:
			return currentMazeFloor + "th Floor";
		}
	}

	public void setFloor(int floorInt)
	{
		currentMazeFloor = floorInt;
	}
}