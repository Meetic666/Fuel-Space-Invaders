using UnityEngine;
using System.Collections;

public class ScreenShakeManager : MonoBehaviour 
{
	public float m_ShakeForce;
	public float m_ShakeTime;
	public float m_ShakeSpeed;

	float m_ActualShake;
	float m_ShakeTimer;
	Vector3 m_InitialPosition;

	void Start()
	{
		m_InitialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 targetPosition = m_InitialPosition;

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

	public void StartShake()
	{
		m_ShakeTimer = m_ShakeTime;

		m_ActualShake += m_ShakeForce;
	}
}
