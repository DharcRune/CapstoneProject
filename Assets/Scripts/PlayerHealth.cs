using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
	public int startingHealth = 100;                            
	public float currentHealth;
	public Slider healthSlider;
	public Image damageImage;  
	public Text healthText;
	//public AudioClip deathClip;
	public AudioClip hurtClip;
	public float flashSpeed = 5f;                               
	public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	
	
	//Animator anim;
	//AudioSource deathAudio;
	AudioSource hurtAudio;
	bool isDead;
	bool damaged;
	
	
	void Start()
	{
		hurtAudio = GetComponent<AudioSource>();
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		damageImage = GameObject.Find("DamageImage").GetComponent<Image>();

		currentHealth = startingHealth;
		healthSlider.value = startingHealth;
		healthText.text = "" + (int)startingHealth;
	}
	
	
	void Update ()
	{
		if(damaged)
		{
			damageImage.color = flashColour;
		}
		else
		{
			damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		damaged = false;
	}
	
	
	public void TakeDamage(float amount)
	{
		damaged = true;
		currentHealth -= amount;
		healthSlider.value = currentHealth;
		healthText.text = "" + (int)currentHealth;

		hurtAudio.clip = hurtClip;
		hurtAudio.Play ();

		if(currentHealth <= 0 && !isDead)
		{
			Death();
		}
	}
	
	
	void Death ()
	{
		isDead = true;
		
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
	}       
}