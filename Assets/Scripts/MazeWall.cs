using UnityEngine;
using System.Collections;

public class MazeWall : MazeCellEdge 
{
	public Transform wall;
	public Material[] wallMaterial;
	private FloorManager fm;

	void Awake()
	{
		fm = GameObject.Find ("FloorManager").GetComponent<FloorManager>();
	}

	public override void Initialize(MazeCell cell, MazeCell otherCell, MazeDirection direction)
	{
		base.Initialize(cell, otherCell, direction);
		//wall.GetComponent<Renderer>().material = cell.room.settings.wallMaterial;

		wall.GetComponent<Renderer>().material = wallMaterial[fm.startingRoomType];
	}
}