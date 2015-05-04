using UnityEngine;
using System.Collections;

public class HudArrow : MonoBehaviour 
{         
	public Transform player;
	
	public void Start()
	{
		player = GameObject.Find("Player(Clone)").transform;
	}
	
	void FixedUpdate() 
	{
		float playerX = player.transform.position.x;
		float playerZ = player.transform.position.z;

		transform.position = new Vector3(playerX, transform.position.y, playerZ);
		transform.rotation = player.transform.rotation * Quaternion.Euler(0, 180, 0);
	}
}
