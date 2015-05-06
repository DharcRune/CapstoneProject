using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour
{
	public Transform overheadCamera;
	
	public void Start()
	{
		//overheadCamera = GameObject.Find("Player Overhead Camera").transform;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		Debug.Log("Player fell through a pitfall!");

		overheadCamera.transform.localPosition = new Vector3(0f, overheadCamera.transform.localPosition.y - 10f, 0f);
	}
}
