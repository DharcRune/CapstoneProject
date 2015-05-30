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
	public AudioSource hurtAudio;
	bool damaged;
	
	
	void Start()
	{
		healthSlider = GameObject.Find("HealthSlider").GetComponent<Slider>();
		healthText = GameObject.Find("HealthText").GetComponent<Text>();
		damageImage = GameObject.Find("DamageImage").GetComponent<Image>();

		currentHealth = startingHealth;
		healthSlider.value = startingHealth;
		healthText.text = "" + (int)startingHealth;
		hurtAudio.clip = hurtClip;
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

		hurtAudio.PlayDelayed(0.25f);

		if(currentHealth <= 0)
		{
			Death();
		}
	}
	
	
	void Death ()
	{	
		Application.LoadLevel("GameOverScene");
	}       
}