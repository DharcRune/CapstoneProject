using UnityEngine;
using System.Collections;

public class DamagingTraps : MonoBehaviour 
{
	public float timeBetweenAttacks = 0.5f;
	public float attackDamage = 10;
	private AudioSource trapSetOff;

	GameObject player;
	Player playerInstance;
	PlayerHealth playerHealth;
	bool playerInRange;
	float timer;
	
	
	void Start()
	{
		player = GameObject.Find("Player(Clone)").gameObject;
		playerInstance = GameObject.Find ("Player(Clone)").GetComponent<Player>();
		playerHealth = player.GetComponent <PlayerHealth>();
		trapSetOff = GetComponent<AudioSource>();
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
			trapSetOff.Play();
			Attack();
			playerInstance.walkingAudio.clip = playerInstance.walkingClip[playerInstance.fm.startingRoomType];
			playerInstance.walkingAudio.Play();
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
