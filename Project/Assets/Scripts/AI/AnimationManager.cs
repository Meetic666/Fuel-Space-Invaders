using UnityEngine;
using System.Collections;

public class AnimationManager : MonoBehaviour 
{
	public GameObject[] m_AnimationFrames;

	int m_CurrentAnimation;

	public void SwitchAnimation()
	{
		m_AnimationFrames[m_CurrentAnimation].SetActive(false);

		m_CurrentAnimation++;

		m_CurrentAnimation %= m_AnimationFrames.Length;

		m_AnimationFrames[m_CurrentAnimation].SetActive(true);
	}
}
