using UnityEngine;
using System.Collections;

public class MazeStairs : MonoBehaviour
{
	public GameManager gm;
	
	public void Start()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Player is on the stairs.");
		gm.increaseFloorIndexByOne();
		gm.increaseSeedIndexByOne ();
		Application.LoadLevel(0);
	}
}