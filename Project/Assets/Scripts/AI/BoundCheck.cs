using UnityEngine;
using System.Collections;

public class BoundCheck : MonoBehaviour 
{
	public float m_MaxX;
	public float m_InvasionCompleteY;

	WaveManager m_Manager;

	float m_PreviousPositionX;

	// Use this for initialization
	void Start () 
	{
		m_Manager = transform.root.GetComponent<WaveManager>();

		m_PreviousPositionX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for side bounds
		if(Mathf.Abs (m_PreviousPositionX) < m_MaxX && Mathf.Abs (transform.position.x) >= m_MaxX)
		{
			m_Manager.ChangeDirection();
		}

		if(transform.position.y <= m_InvasionCompleteY)
		{
			m_Manager.Stop ();
		}

		m_PreviousPositionX = transform.position.x;
	}
}
