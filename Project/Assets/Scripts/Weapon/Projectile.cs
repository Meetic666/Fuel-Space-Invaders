using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	public float m_Speed;
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 previousPosition = transform.position;

		transform.position += transform.up * m_Speed * Time.deltaTime;

		Vector3 displacement = transform.position - previousPosition;

		RaycastHit hitInfo;

		if(Physics.Raycast(transform.position, displacement, out hitInfo, displacement.magnitude))
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
