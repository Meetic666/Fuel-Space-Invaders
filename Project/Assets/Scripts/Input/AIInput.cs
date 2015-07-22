using UnityEngine;
using System.Collections;

// AIInput handles the input set by the AI (in turn managing firing missiles)
public class AIInput : BaseInput
{
	#region Members Open For Designer
	public float m_FiringTime;
	#endregion

	#region Private Members
	float m_FiringTimer;
	#endregion

	#region Unity Hooks
	// Use this for initialization
	void Start () 
	{
		SetTimer ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Counts down timer until the enemy inputs a fire command
		m_FiringTimer -= Time.deltaTime;

		if(m_FiringTimer < 0.0f)
		{
			Fire = true;

			SetTimer ();
		}
		else
		{
			Fire = false;
		}
	}
	#endregion

	#region Private Methods
	void SetTimer()
	{
		// Sets timer before next shot to be random between 0 and firing time
		m_FiringTimer = Random.Range (0.0f, m_FiringTime);
	}
	#endregion
}
