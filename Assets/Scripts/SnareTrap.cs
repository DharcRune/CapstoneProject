using UnityEngine;
using System.Collections;

public class SnareTrap : MonoBehaviour 
{
	public float timeBetweenAttacks = 6f;
	public float attackDamage = 0;
	private AudioSource trapSetOff;

	GameObject playerGameObject; 
	Player player;
	bool playerInRange;
	float timer;
	
	
	void Start()
	{
		playerGameObject = GameObject.Find("Player(Clone)").gameObject;
		player = playerGameObject.GetComponent<Player>();
		trapSetOff = GetComponent<AudioSource>();
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
			trapSetOff.Play();
			Snare();
		}
	}
	
	
	void Snare ()
	{
		timer = 0f;

		player.toggleKeyInput ();
	}
}
