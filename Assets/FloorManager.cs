using UnityEngine;
using System.Collections;

public class FloorManager : MonoBehaviour 
{
	public int seedIndex;
	public int floorIndex;

	void Awake() 
	{
		DontDestroyOnLoad(transform.gameObject);
	}

	// Use this for initialization
	void Start() 
	{
		seedIndex = 0;
		floorIndex = 1;
	}
	
	// Update is called once per frame
	void Update() 
	{
	
	}

	public void StartGame()
	{
		Application.LoadLevel(1);
	}
}
