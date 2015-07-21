using UnityEngine;
using System.Collections;

public class LifeTime : MonoBehaviour 
{
	public float m_LifeTime;

	float m_Timer;

	void OnEnable()
	{
		m_Timer = m_LifeTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_Timer -= Time.deltaTime;

		if(m_Timer < 0.0f)
		{
			gameObject.SetActive (false);
		}
	}
}
