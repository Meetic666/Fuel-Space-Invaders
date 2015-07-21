using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float m_Speed;

	public LayerMask m_PhysicsLayersToCollideWith;
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 previousPosition = transform.position;

		transform.position += transform.up * m_Speed * Time.deltaTime;

		Vector3 displacement = transform.position - previousPosition;

		RaycastHit hitInfo;

		if(Physics.SphereCast(previousPosition + transform.up * transform.localScale.y * 0.5f, transform.localScale.x, displacement, out hitInfo, displacement.magnitude, m_PhysicsLayersToCollideWith.value))
		{
			Health otherHealth = hitInfo.collider.GetComponent<Health>();

			if(otherHealth)
			{
				otherHealth.Damage();

				gameObject.SetActive (false);
			}
		}
	}
}
