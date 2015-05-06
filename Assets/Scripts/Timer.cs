using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour 
{
	public Text text;
	private float timer;
	private float minutes;
	private float seconds;

	void Start()
	{
		timer = 0;
	}

	void Update() 
	{
		timer += Time.deltaTime;

		minutes = Mathf.FloorToInt(timer/60f);
		seconds = Mathf.FloorToInt(timer - minutes * 60);

		text.text = string.Format("{0:00}:{1:00}", minutes, seconds);	
	}
}