using UnityEngine;
using System.Collections;

public class MouseCamera : MonoBehaviour 
{	
	void Update() 
	{

		transform.rotation = Quaternion.Euler(0, Input.mousePosition.x, 0);
	}
}