using UnityEngine;
using System.Collections;

// DigitManager handles what numeral the digit object is displaying
public class DigitManager : MonoBehaviour 
{
	#region Public Methods
	// Activates the proper digit to show and deactivates the other ones
	// The digits are child objects of this object (better have 0 through 9)
	public void SetDigit(int digitValue)
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(i == digitValue);
		}
	}
	#endregion
}
