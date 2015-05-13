using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour
{
	public GameManager gm;
	
	public void Start()
	{
		gm = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Player fell through a pitfall!");
		gm.lowerFloorIndexByOne();
		gm.lowerSeedIndexByOne();
		gm.RestartGame();
	}
}
