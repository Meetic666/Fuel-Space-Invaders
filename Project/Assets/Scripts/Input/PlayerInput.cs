using UnityEngine;
using System.Collections;

// PlayerInput handles the input set by the player, using mouse and keyboard input
public class PlayerInput : BaseInput 
{	
	#region Private Members
	bool m_IsUsingKeyboardForMovement;

	Vector3 m_PreviousMousePosition;
	#endregion

	#region Unity Hooks
	// Update is called once per frame
	void Update () 
	{
		Fire = Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space);

		if(!m_IsUsingKeyboardForMovement)
		{
			TargetPosition = CalculateMousePosition();
		}

		if(Input.GetKey(KeyCode.LeftArrow))
		{
			TargetPosition -= 1;

			m_IsUsingKeyboardForMovement = true;
		}

		if(Input.GetKey(KeyCode.RightArrow))
		{
			TargetPosition += 1;

			m_IsUsingKeyboardForMovement = true;
		}

		if(Input.mousePosition != m_PreviousMousePosition)
		{
			m_IsUsingKeyboardForMovement = false;
		}

		m_PreviousMousePosition = Input.mousePosition;
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
