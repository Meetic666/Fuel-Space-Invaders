using UnityEngine;
using System.Collections;

public class DigitManager : MonoBehaviour 
{
	public void SetDigit(int digitValue)
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == digitValue);
		}
	}
}
