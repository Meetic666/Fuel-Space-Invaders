using UnityEngine;
using System.Collections;

public class BaseManager : MonoBehaviour 
{
	public void Reset()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(true);
		}
	}
}
