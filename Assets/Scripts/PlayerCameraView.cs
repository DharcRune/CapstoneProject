using UnityEngine;
using System.Collections;

public class PlayerCameraView : MonoBehaviour 
{
	public Transform player;
	
	public void Start()
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
