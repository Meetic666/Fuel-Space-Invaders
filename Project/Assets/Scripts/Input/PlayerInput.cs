using UnityEngine;
using System.Collections;

// PlayerInput handles the input set by the player, using mouse input
public class PlayerInput : BaseInput 
{	
	#region Unity Hooks
	// Update is called once per frame
	void Update () 
	{
		Fire = Input.GetMouseButton(0);

		TargetPosition = CalculateMousePosition();
	}
	#endregion

	#region Private Methods
	// Calculates the world position of the cursor in the plane of the player
	float CalculateMousePosition()
	{
		Vector3 mousePosition = Input.mousePosition;
		mousePosition.z = transform.position.z - Camera.main.transform.position.z;

		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

		return mouseWorldPosition.x;
	}
	#endregion
}
