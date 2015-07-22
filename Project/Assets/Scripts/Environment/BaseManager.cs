using UnityEngine;
using System.Collections;

// BaseManager handles the reconstruction of base shields when game is reset
public class BaseManager : MonoBehaviour 
{
	#region Public Methods
	// Resets all child objects to be active (in turn reconstructing the base shields)
	public void Reset()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(true);
		}
	}
	#endregion
}
