using UnityEngine;
using System.Collections;

public class AIInput : BaseInput
{
	public float m_FiringTime;

	float m_FiringTimer;

	// Use this for initialization
	void Start () 
	{
		m_FiringTimer = Random.Range (0.0f, m_FiringTime);
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_FiringTimer -= Time.deltaTime;

		if(m_FiringTimer < 0.0f)
		{
			Fire = true;

			m_FiringTimer = Random.Range (0.0f, m_FiringTime);
		}
		else
		{
			Fire = false;
		}
	}
}
