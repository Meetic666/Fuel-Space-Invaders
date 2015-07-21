using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public GameObject m_DestructionParticlesPrefab;

	ObjectPool m_ObjectPool;

	void Start()
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}

	public void Damage()
	{
		gameObject.SetActive (false);

		GameObject newObject = m_ObjectPool.Instantiate (m_DestructionParticlesPrefab, transform.position, Quaternion.identity);

		newObject.GetComponent<ParticleSystem>().Clear ();
	}
}
