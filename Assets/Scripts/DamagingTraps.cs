using UnityEngine;
using System.Collections;

public class DamagingTraps : MonoBehaviour 
{
	public float timeBetweenAttacks = 0.5f;
	public float attackDamage = 10;

	GameObject player;
	PlayerHealth playerHealth;
	bool playerInRange;
	float timer;
	
	
	void Start()
	{
		player = GameObject.Find("Player(Clone)").gameObject;
		playerHealth = player.GetComponent <PlayerHealth>();
		timer = 0f;
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
		{
			playerInRange = false;
		}
	}
	
	
	void Update ()
	{
		timer += Time.deltaTime;
		
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			Attack ();
		}
		
		//if(playerHealth.currentHealth <= 0)
		//{

		//}
	}
	
	
	void Attack ()
	{
		timer = 0f;

		if(playerHealth.currentHealth > 0)
		{
			playerHealth.TakeDamage(attackDamage);
		}
	}
}
