using UnityEngine;
using System.Collections;

// BoundCheck manages the enemy's behaviour when it goes out of bounds laterally
// as well as when it touches ground
public class BoundCheck : MonoBehaviour 
{
	#region Members Open For Designer
	public float m_MaxX;
	public float m_InvasionCompleteY;
	#endregion

	#region Private Members
	WaveManager m_Manager;

	float m_PreviousPositionX;
	#endregion

	#region Unity Hooks
	// Use this for initialization
	void Start () 
	{
		m_Manager = transform.root.GetComponent<WaveManager>();

		m_PreviousPositionX = transform.position.x;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check for entering side bounds
		if(Mathf.Abs (m_PreviousPositionX) < m_MaxX && Mathf.Abs (transform.position.x) >= m_MaxX)
		{
			// Tells manager to change direction of movement
			m_Manager.ChangeDirection();
		}

		// Chek for losing condition (well, losing on the player's side muhahahaha)
		if(transform.position.y <= m_InvasionCompleteY)
		{
			// Tells wave manager to stop and go into game over
			m_Manager.Stop ();
		}

		m_PreviousPositionX = transform.position.x;
	}
	#endregion
}
