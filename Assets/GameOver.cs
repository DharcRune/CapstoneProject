using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
	void Start()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}

	void Update() 
	{
	
	}

	public void clicked()
	{
		Debug.Log ("Click");
	}

	public void LoadMainMenu()
	{
		Application.LoadLevel("StartScene");
	}	
}
