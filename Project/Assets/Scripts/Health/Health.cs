using UnityEngine;
using System.Collections;

// Health handles the object's behaviour when getting hit
public class Health : MonoBehaviour 
{
	#region Members Open For Designer
	public GameObject m_DestructionParticlesPrefab;
	#endregion

	#region Private Members
	ObjectPool m_ObjectPool;

	ScreenShakeManager m_ScreenShakeManager;
	#endregion

	#region Unity Hooks
	// Use this for initialization
	void Start()
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();

		m_ScreenShakeManager = FindObjectOfType<ScreenShakeManager>();
	}
	#endregion

	#region Public Methods
	// Damage is called whenever the object is hit
	public void Damage()
	{
		gameObject.SetActive (false);

		GameObject newObject = m_ObjectPool.Instantiate (m_DestructionParticlesPrefab, transform.position, Quaternion.identity);

		// Resets the particle system to explode again
		newObject.GetComponent<ParticleSystem>().Clear ();

		m_ScreenShakeManager.StartShake();
	}

	public void Reset()
	{
		gameObject.SetActive(true);
	}
	#endregion
}
