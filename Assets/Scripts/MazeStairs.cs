using UnityEngine;
using System.Collections;

public class MazeStairs : MonoBehaviour
{
	private Transform overheadCamera;
	
	public void Start()
	{
		overheadCamera = GameObject.Find("Player Overhead Camera").transform;
	}

	void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Player is on the stairs.");

		collider.transform.localPosition = new Vector3(0f, collider.transform.localPosition.y + 10f, 0f);
		overheadCamera.transform.localPosition = new Vector3(0f, overheadCamera.transform.localPosition.y + 10f, 0f);
	}
}