using UnityEngine;
using System.Collections;

// PlayerMovement handles the player movement based on the input
public class PlayerMovement : MonoBehaviour 
{
	#region Members Open For Design
	public float m_Speed;
	public float m_MaxX;
	#endregion

	#region Private Members
	BaseInput m_Input;
	#endregion

	#region Unity Hooks
	void Start()
	{
		m_Input = GetComponent<BaseInput>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Gets the target position given by input, and sets it in between the allotted bounds
		float actualTargetPosition = Mathf.Clamp(m_Input.TargetPosition, - m_MaxX, m_MaxX);

		// Moves the object towards the target position using the set speed
		Vector3 newPosition = transform.position;
		newPosition.x = Mathf.Lerp (newPosition.x, actualTargetPosition, m_Speed * Time.deltaTime);
		transform.position = newPosition;
	}
	#endregion
}
