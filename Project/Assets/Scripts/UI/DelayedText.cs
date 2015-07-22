using UnityEngine;
using System.Collections;

// DelayedText handles text that appears over time using a set characer per second rate
public class DelayedText : MonoBehaviour 
{
	#region Members Open For Designer
	public float m_CharacterPerSecond;
	#endregion

	#region Private Members
	float m_NumberOfCharactersDisplayed;

	bool m_FirstInitialization;
	#endregion

	#region Unity Hooks
	void Start()
	{
		m_FirstInitialization = true;

		UpdateDisplay ();
	}

	void OnEnable()
	{
		if(m_FirstInitialization)
		{
			m_NumberOfCharactersDisplayed = 0.0f;
			
			UpdateDisplay ();
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Increase the number of characters to display using the character rate
		m_NumberOfCharactersDisplayed += m_CharacterPerSecond * Time.deltaTime;

		UpdateDisplay ();
	}
	#endregion

	#region Private Methods
	void UpdateDisplay()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			// If the character is at an index lesser than the number of characters to display
			// Then the character is set active
			// Else it is set inactive
			transform.GetChild(i).gameObject.SetActive(i <= m_NumberOfCharactersDisplayed);
		}
	}
	#endregion
}
