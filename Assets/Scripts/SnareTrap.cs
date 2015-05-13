﻿using UnityEngine;
using System.Collections;

public class SnareTrap : MonoBehaviour 
{
	public float timeBetweenAttacks = 6f;     // The time in seconds between each attack.
	public float attackDamage = 0;               // The amount of health taken away per attack.
	
	
	//Animator anim;                              // Reference to the animator component.
	GameObject playerGameObject;                          // Reference to the player GameObject.
	Player player;
	PlayerHealth playerHealth;                  // Reference to the player's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.
	
	
	void Start()
	{
		// Setting up the references.
		playerGameObject = GameObject.Find("Player(Clone)").gameObject;
		player = playerGameObject.GetComponent<Player>();
		playerHealth = playerGameObject.GetComponent<PlayerHealth>();
		timer = 0f;
		//anim = GetComponent <Animator> ();
	}
	
	
	void OnTriggerEnter(Collider other)
	{
		// If the entering collider is the player...
		if(other.gameObject == playerGameObject)
		{
			// ... the player is in range.
			playerInRange = true;
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == playerGameObject)
		{
			// ... the player is no longer in range.
			playerInRange = false;
		}
	}
	
	
	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if(timer >= timeBetweenAttacks && playerInRange)
		{
			// ... attack.
			Snare();
		}
		
		// If the player has zero or less health...
		if(playerHealth.currentHealth <= 0)
		{
			// ... tell the animator the player is dead.
			//anim.SetTrigger ("PlayerDead");
		}
	}
	
	
	void Snare ()
	{
		Debug.Log("Snaring");

		// Reset the timer.
		timer = 0f;
		
		// If the player has health to lose...
		if(playerHealth.currentHealth > 0)
		{
			// ... damage the player.
			playerHealth.TakeDamage(attackDamage);
		}

		player.toggleKeyInput ();
	}
}
