using UnityEngine;
using System.Collections;

public class SnareTrap : MonoBehaviour 
{
	public float timeBetweenAttacks = 6f;
	public float attackDamage = 0;

	GameObject playerGameObject; 
	Player player;
	bool playerInRange;
	float timer;
	
	
	void Start()
	{
		playerGameObject = GameObject.Find("Player(Clone)").gameObject;
		player = playerGameObject.GetComponent<Player>();
		timer = 0f;
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == playerGameObject)
		{
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == playerGameObject)
		{
			playerInRange = false;
		}
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			Snare();
		}
	}
	
	
	void Snare ()
	{
		timer = 0f;

		player.toggleKeyInput ();
	}
}
