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
	private bool allowKeyInput;
	private AudioSource walkingAudio;

	public void Start()
	{
		allowKeyInput = true;
		distance = 1f;
		cam = GetComponentInChildren<Camera>();
		walkingAudio = GetComponent<AudioSource>();
	}

	public void SetLocation(MazeCell cell)
	{
		currentCell = cell;
		transform.localPosition = new Vector3(cell.transform.localPosition.x, 0.5f, cell.transform.localPosition.z);
	}

	public void SetLocation(IntVector2 position)
	{
		transform.localPosition = new Vector3(position.x, 0.5f, position.z);
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
	}

	private void Update() 
	{
		if(allowKeyInput) 
		{
			if(Input.GetKey(KeyCode.W)) 
			{
				if(Input.GetKeyDown(KeyCode.W))
				{
					walkingAudio.Play();
				}

				movement = transform.position + cam.transform.forward * distance * Time.deltaTime;
				transform.position = new Vector3 (movement.x, transform.position.y, movement.z);
			} 
			else if(Input.GetKeyUp(KeyCode.W))
			{
				walkingAudio.Stop();
			}

			else if(Input.GetKey(KeyCode.D)) 
			{
				if(Input.GetKeyDown(KeyCode.D))
				{
					walkingAudio.Play();
				}

				movement = transform.position + cam.transform.right * distance * Time.deltaTime;
				transform.position = new Vector3 (movement.x, transform.position.y, movement.z);
			} 
			else if(Input.GetKeyUp(KeyCode.D))
			{
				walkingAudio.Stop();
			}


			else if(Input.GetKey(KeyCode.S)) 
			{
				if(Input.GetKeyDown(KeyCode.S))
				{
					walkingAudio.Play();
				}

				movement = transform.position - cam.transform.forward * distance * Time.deltaTime;
				transform.position = new Vector3 (movement.x, transform.position.y, movement.z);
			}
			else if(Input.GetKeyUp(KeyCode.S))
			{
				walkingAudio.Stop();
			}


			else if(Input.GetKey(KeyCode.A)) 
			{
				if(Input.GetKeyDown(KeyCode.A))
				{
					walkingAudio.Play();
				}

				movement = transform.position - cam.transform.right * distance * Time.deltaTime;
				transform.position = new Vector3 (movement.x, transform.position.y, movement.z);
			}
			else if(Input.GetKeyUp(KeyCode.A))
			{
				walkingAudio.Stop();
			}

		}

		Rotate();
	}

	public void toggleKeyInput()
	{
		allowKeyInput = !allowKeyInput;
	}
}
