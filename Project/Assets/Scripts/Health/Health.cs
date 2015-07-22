using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public GameObject m_DestructionParticlesPrefab;

	ObjectPool m_ObjectPool;

	ScreenShakeManager m_ScreenShakeManager;

	void Start()
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();

		m_ScreenShakeManager = FindObjectOfType<ScreenShakeManager>();
	}

	public void Damage()
	{
		gameObject.SetActive (false);

		GameObject newObject = m_ObjectPool.Instantiate (m_DestructionParticlesPrefab, transform.position, Quaternion.identity);

		newObject.GetComponent<ParticleSystem>().Clear ();

		m_ScreenShakeManager.StartShake();
	}
}
