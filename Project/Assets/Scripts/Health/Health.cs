using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{
	public void Damage()
	{
		gameObject.SetActive (false);
	}
}
