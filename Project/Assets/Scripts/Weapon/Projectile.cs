using UnityEngine;
using System.Collections;

// Projectile handles the movement and collision of projectile objects
public class Projectile : MonoBehaviour 
{
	#region Members Open For Designer
	public float m_Speed;
	public GameObject m_FiringSoundPrefab;
	public GameObject m_ExplosionSoundPrefab;

	public LayerMask m_PhysicsLayersToCollideWith;
	#endregion

	#region Private Members
	ObjectPool m_ObjectPool;
	#endregion

	#region Unity Hooks
	void Awake()
	{
		m_ObjectPool = FindObjectOfType<ObjectPool>();
	}

	void OnEnable()
	{
		// Gets appropriate firing sound from object pool and plays the sound
		GameObject newSoundObject = m_ObjectPool.Instantiate(m_FiringSoundPrefab, transform.position, Quaternion.identity);
		newSoundObject.GetComponent<AudioSource>().Play ();
	}

	// Update is called once per frame
	void Update ()
	{
		Vector3 previousPosition = transform.position;

		transform.position += transform.up * m_Speed * Time.deltaTime;

		Vector3 displacement = transform.position - previousPosition;

		CheckForCollision(displacement);
	}
	#endregion

	#region Private Methods
	void CheckForCollision(Vector3 displacement)
	{
		RaycastHit hitInfo;

		Vector3 previousTipOfProjectilePosition = transform.position - displacement + transform.up * transform.localScale.y * 0.5f;

		// Checks collision using sphere cast, to get a bigger hit box
		if(Physics.SphereCast(previousTipOfProjectilePosition, transform.localScale.x, displacement, out hitInfo, displacement.magnitude, m_PhysicsLayersToCollideWith.value))
		{
			Health otherHealth = hitInfo.collider.GetComponent<Health>();
			
			if(otherHealth)
			{
				otherHealth.Damage();
				
				Explode ();
			}
		}
	}

	void Explode()
	{
		gameObject.SetActive (false);
		
		GameObject newSoundObject = m_ObjectPool.Instantiate(m_ExplosionSoundPrefab, transform.position, Quaternion.identity);
		newSoundObject.GetComponent<AudioSource>().Play ();
	}
	#endregion
}
