using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInput 
{	
	// Update is called once per frame
	void Update () 
	{
		Fire = Input.GetMouseButton(0);

		TargetPosition = CalculateMousePosition();
	}

	float CalculateMousePosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = transform.position.z - Camera.main.transform.position.z;

		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		return mouseWorldPosition.x;
	}
}
