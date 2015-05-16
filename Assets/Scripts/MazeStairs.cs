using UnityEngine;
using System.Collections;

public class MazeStairs : MonoBehaviour
{
	public FloorManager fm;
	
	public void Start()
	{
		fm = GameObject.Find("FloorManager").GetComponent<FloorManager>();
	}

	void OnTriggerEnter(Collider collider)
	{
		fm.increaseFloorIndexByOne();
		fm.increaseSeedIndexByOne();

		if(fm.changeFloorIndex >= fm.maxFloorChanges)
		{
			fm.increaseChangeFloorIndexByOne();
		}

		Application.LoadLevel("LoadingUpperFloor");
	}
}