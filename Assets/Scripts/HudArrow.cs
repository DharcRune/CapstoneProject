using UnityEngine;
using System.Collections;

public class HudArrow : MonoBehaviour 
{         
	public Transform player;
	private float playerX;
	private float playerZ;
	
	public void startHudArrow()
	{
		player = GameObject.Find("Player(Clone)").transform;
	}
	
	
	void FixedUpdate() 
	{
		playerX = player.transform.position.x;
		playerZ = player.transform.position.z;

		transform.position = new Vector3(playerX, transform.position.y, playerZ);
		transform.rotation = player.transform.rotation * Quaternion.Euler(0, 180, 0);
	}
}
