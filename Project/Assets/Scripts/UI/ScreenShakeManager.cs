using UnityEngine;
using System.Collections;

// ScreenShakeManager handles all the shake that the screen can take !!!
// Let's make Vlambeer proud ;-)
public class ScreenShakeManager : MonoBehaviour 
{
	#region Members Open For Designer
	public float m_ShakeForce;
	public float m_ShakeTime;
	public float m_ShakeSpeed;
	public float m_MaxShake;
	#endregion

	#region Private Members
	float m_ActualShake;
	float m_ShakeTimer;
	Vector3 m_InitialPosition;
	#endregion

	#region Unity Hooks
	// Use this for initialization
	void Start()
	{
		m_InitialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetPosition = m_InitialPosition;

		// Checks if screen is shaking
		if(m_ShakeTimer > 0.0f)
		{
			m_ShakeTimer -= Time.deltaTime;

			targetPosition += Random.insideUnitSphere * m_ActualShake;
		}
		else
		{
			m_ActualShake = 0.0f;
		}

		transform.position = Vector3.Lerp(transform.position, targetPosition, m_ShakeSpeed * Time.deltaTime);
	}
	#endregion

	#region Public Methods
	// Start shake with multiplier applied to m_ShakeForce
	public void StartShake(float shakeMultiplier)
	{
		m_ShakeTimer = m_ShakeTime;

		m_ActualShake += m_ShakeForce * shakeMultiplier;

		if(m_ActualShake > m_MaxShake)
		{
			m_ActualShake = m_MaxShake;
		}
	}

	public void StartShake()
	{
		// Start shake with base shake value
		StartShake (1.0f);
	}
	#endregion
}
