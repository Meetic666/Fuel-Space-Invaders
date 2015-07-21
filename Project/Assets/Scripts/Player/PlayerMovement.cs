using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour 
{
	public float m_Speed;
	public float m_MaxX;

	BaseInput m_Input;

	void Start()
	{
		m_Input = GetComponent<BaseInput>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		float actualTargetPosition = Mathf.Clamp(m_Input.TargetPosition, - m_MaxX, m_MaxX);

		Vector3 newPosition = transform.position;
		newPosition.x = Mathf.Lerp (newPosition.x, actualTargetPosition, m_Speed * Time.deltaTime);
		transform.position = newPosition;
	}
}
