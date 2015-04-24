using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	private MazeCell currentCell;
	private MazeDirection currentDirection;

	public void SetLocation(MazeCell cell)
	{
		currentCell = cell;
		transform.localPosition = cell.transform.localPosition;
	}

	private void Move(MazeDirection direction)
	{
		MazeCellEdge edge = currentCell.GetEdge (direction);

		if(edge is MazePassage)
		{
			SetLocation(edge.otherCell);
		}
	}

	private void Rotate(MazeDirection direction)
	{
		transform.localRotation = direction.ToRotation();
		currentDirection = direction;
	}

	private void Update() 
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			Move(currentDirection);
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			Move(currentDirection.GetNextClockwise());
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			Move(currentDirection.GetOpposite());
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			Move(currentDirection.GetNextCounterclockwise());
		}
		else if(Input.GetKeyDown(KeyCode.Q))
		{
			Rotate(currentDirection.GetNextCounterclockwise());
		}
		else if(Input.GetKeyDown(KeyCode.E))
		{
			Rotate(currentDirection.GetNextClockwise());
		}
	}
}
