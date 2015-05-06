using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour
{
	private Transform player;

	public void startCamera()
	{
		player = GameObject.Find("Player(Clone)").transform;
	}
	
	
	void FixedUpdate()
	{
		if (player != null) 
		{
			float playerX = player.transform.position.x;
			float playerZ = player.transform.position.z;
			
			transform.position = new Vector3(playerX, transform.position.y, playerZ);
		} 
		else
		{
			player = GameObject.Find("Player(Clone)").transform;
		}
	}
}