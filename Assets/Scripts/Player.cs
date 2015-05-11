using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour 
{
	private Camera cam;
	private float distance;
	private MazeCell currentCell;
	private MazeDirection currentDirection;
	private Vector3 movement;

	public void Start()
	{
		distance = 1f;
		cam = GetComponentInChildren<Camera>();
	}

	public void SetLocation(MazeCell cell)
	{
		currentCell = cell;
		transform.localPosition = new Vector3(cell.transform.localPosition.x, 0.5f, cell.transform.localPosition.z);
	}

	private void Move(MazeDirection direction)
	{
		MazeCellEdge edge = currentCell.GetEdge(direction);

		if(edge is MazePassage)
		{
			SetLocation(edge.otherCell);
		}
	}

	private void Rotate()
	{
		transform.rotation = Quaternion.Euler(0, Input.mousePosition.x, 0);
		//transform.localRotation = direction.ToRotation();
		//currentDirection = direction;
	}

	private void Update() 
	{
		if(Input.GetKey(KeyCode.W))
		{
			movement = transform.position + cam.transform.forward * distance * Time.deltaTime;
			transform.position = new Vector3(movement.x, transform.position.y, movement.z);
		}
		else if(Input.GetKey(KeyCode.D))
		{
			movement = transform.position + cam.transform.right * distance * Time.deltaTime;
			transform.position = new Vector3(movement.x, transform.position.y, movement.z);
		}
		else if(Input.GetKey(KeyCode.S))
		{
			movement = transform.position - cam.transform.forward * distance * Time.deltaTime;
			transform.position = new Vector3(movement.x, transform.position.y, movement.z);
		}
		else if(Input.GetKey(KeyCode.A))
		{
			movement = transform.position - cam.transform.right * distance * Time.deltaTime;
			transform.position = new Vector3(movement.x, transform.position.y, movement.z);
		}

		Rotate();
	}
}
