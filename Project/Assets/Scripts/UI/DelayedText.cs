using UnityEngine;
using System.Collections;

public class DelayedText : MonoBehaviour 
{
	public float m_CharacterPerSecond;

	float m_NumberOfCharactersDisplayed;

	// Use this for initialization
	void Start () 
	{
		UpdateDisplay ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		m_NumberOfCharactersDisplayed += m_CharacterPerSecond * Time.deltaTime;

		UpdateDisplay ();
	}

	void UpdateDisplay()
	{
		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(i <= m_NumberOfCharactersDisplayed);
		}
	}
}
