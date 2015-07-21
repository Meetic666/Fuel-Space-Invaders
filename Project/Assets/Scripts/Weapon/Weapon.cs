using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour 
{
	public GameObject m_ProjectilePrefab;
	public float m_FiringRate;
	public Transform m_FiringPoint;

	float m_FiringTimer;

	BaseInput m_Input;

	ObjectPool m_ObjectPool;

	// Use this for initialization
	void Start () 
	{
		m_Input = GetComponent<BaseInput>();

		m_ObjectPool = FindObjectOfType<ObjectPool>();
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

	void Fire()
	{
		m_ObjectPool.Instantiate(m_ProjectilePrefab, m_FiringPoint.position, m_FiringPoint.rotation);

		m_FiringTimer = 1.0f / m_FiringRate;
	}
}
