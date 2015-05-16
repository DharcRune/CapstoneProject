using UnityEngine;
using System.Collections;

public class Pitfall : MonoBehaviour
{
	public FloorManager fm;
	
	public void Start()
	{
		fm = GameObject.Find("FloorManager").GetComponent<FloorManager>();
	}
	
	void OnTriggerEnter(Collider collider)
	{
		fm.lowerFloorIndexByOne();
		fm.lowerSeedIndexByOne();
		fm.resetChangeFloorIndex ();
		Application.LoadLevel(3);
	}
}
