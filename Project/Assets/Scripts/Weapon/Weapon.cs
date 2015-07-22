using UnityEngine;
using System.Collections;

// Weapon handles the creation of projectiles according to firing rate and input
public class Weapon : MonoBehaviour 
{
	#region Members Open For Designer
	public GameObject m_ProjectilePrefab;
	public float m_FiringRate;
	public Transform m_FiringPoint;
	#endregion

	#region Private Members
	float m_FiringTimer;

	BaseInput m_Input;

	ObjectPool m_ObjectPool;

	ScreenShakeManager m_ScreenShakeManager;
	#endregion

	#region Unity Hooks
	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<BaseInput>();

		m_ObjectPool = FindObjectOfType<ObjectPool>();

		m_ScreenShakeManager = FindObjectOfType<ScreenShakeManager>();
	}

	// Update is called once per frame
	void Update () 
	{
		m_FiringTimer -= Time.deltaTime;

		if(m_FiringTimer < 0.0f && m_Input.Fire)
		{
			Fire ();
		}
	}
	#endregion

	#region Private Methods
	void Fire()
	{
		m_ObjectPool.Instantiate(m_ProjectilePrefab, m_FiringPoint.position, m_FiringPoint.rotation);

		m_FiringTimer = 1.0f / m_FiringRate;

		m_ScreenShakeManager.StartShake(0.5f);
	}
	#endregion
}
