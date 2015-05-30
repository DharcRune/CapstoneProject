using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Player : MonoBehaviour 
{
	public AudioClip[] walkingClip;
	private Camera cam;
	private float distance;
	private MazeCell currentCell;
	private MazeDirection currentDirection;
	private Vector3 movement;
	private bool allowKeyInput;
	public AudioSource walkingAudio;
	public FloorManager fm;
	private bool isPause;
	public Text pauseText;
	public Text resumeText;
	public Text quitText;
	private bool jumpWait;


	public void Start()
	{
		if(Time.timeScale != 1)
		{
			Time.timeScale = 1;
		}

		allowKeyInput = true;
		distance = 1f;
		cam = GetComponentInChildren<Camera>();

		fm = GameObject.Find ("FloorManager").GetComponent<FloorManager>();
		pauseText = GameObject.Find("PausedText").GetComponent<Text>();
		resumeText = GameObject.Find("ResumeText").GetComponent<Text>();
		quitText = GameObject.Find("QuitText").GetComponent<Text>();

		walkingAudio.clip = walkingClip[fm.startingRoomType];
		isPause = false;
		jumpWait = false;

		pauseText.enabled = false;
		resumeText.enabled = false;
		quitText.enabled = false;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.name.Equals("Floor"))
		{
			Debug.Log("Player is on floor");
			jumpWait = false;
		}
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
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			isPause = !isPause;

			if(isPause)
			{
				Time.timeScale = 0;
				pauseText.enabled = true;
				resumeText.enabled = true;
				quitText.enabled = true;
				Cursor.visible = true;
			}
			else
			{
				Time.timeScale = 1;
				pauseText.enabled = false;
				resumeText.enabled = false;
				quitText.enabled = false;
				Cursor.visible = false;
			}
		}

		if(isPause)
		{
			if(Input.GetKeyDown(KeyCode.Q))
			{
				Application.LoadLevel("StartScene");
			}
		}

		if(!isPause)
		{
			if(allowKeyInput) 
			{
				if(Input.GetKey(KeyCode.W)) 
				{
					if (Input.GetKeyDown(KeyCode.W)) 
					{
						walkingAudio.Play ();
					}
					
					movement = transform.position + cam.transform.forward * distance * Time.deltaTime;
					transform.position = new Vector3 (movement.x, transform.position.y, movement.z);
				} 
				else if (Input.GetKeyUp (KeyCode.W)) 
				{
					walkingAudio.Stop ();
				} 
				
				else if (Input.GetKey (KeyCode.D)) 
				{
					if (Input.GetKeyDown (KeyCode.D)) 
					{
						walkingAudio.Play ();
					}
					
					movement = transform.position + cam.transform.right * distance * Time.deltaTime;
					transform.position = new Vector3 (movement.x, transform.position.y, movement.z);
				} 
				else if (Input.GetKeyUp (KeyCode.D)) 
				{
					walkingAudio.Stop ();
				} 
				
				else if (Input.GetKey (KeyCode.S)) 
				{
					if (Input.GetKeyDown (KeyCode.S)) 
					{
						walkingAudio.Play ();
					}
					
					movement = transform.position - cam.transform.forward * distance * Time.deltaTime;
					transform.position = new Vector3 (movement.x, transform.position.y, movement.z);
				} 
				else if (Input.GetKeyUp (KeyCode.S)) 
				{
					walkingAudio.Stop ();
				} 
				
				else if (Input.GetKey (KeyCode.A))
				{
					if (Input.GetKeyDown (KeyCode.A))
					{
						walkingAudio.Play ();
					}
					
					movement = transform.position - cam.transform.right * distance * Time.deltaTime;
					transform.position = new Vector3(movement.x, transform.position.y, movement.z);
				} 
				else if (Input.GetKeyUp(KeyCode.A)) 
				{
					walkingAudio.Stop ();
				}

				if(Input.GetKey(KeyCode.Space) && !jumpWait)
				{
					transform.position = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
					jumpWait = true;
				}
			} 
			else
			{
				walkingAudio.Stop();
			}
			
			Rotate();
		}
	}

	public void toggleKeyInput()
	{
		allowKeyInput = !allowKeyInput;
	}

	public void ResumeGame()
	{
		Debug.Log("Clicked");
	}
}
