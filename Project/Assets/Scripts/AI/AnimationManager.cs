using UnityEngine;
using System.Collections;

// Animation manager manages voxel animation for objects
public class AnimationManager : MonoBehaviour 
{
	#region Members Open For Design
	// Animation frames are sub objects of this
	// Used for fancy voxel animation
	public GameObject[] m_AnimationFrames;
	#endregion

	#region Private Members
	int m_CurrentAnimationIndex;
	#endregion

	#region Public Methods
	public void SwitchAnimation()
	{
		// Deactivates previous frame
		m_AnimationFrames[m_CurrentAnimationIndex].SetActive(false);

		// Updates current index, and loops it around if necessary
		m_CurrentAnimationIndex++;

		m_CurrentAnimationIndex %= m_AnimationFrames.Length;

		// Activates current animation frame
		m_AnimationFrames[m_CurrentAnimationIndex].SetActive(true);
	}
	#endregion
}
