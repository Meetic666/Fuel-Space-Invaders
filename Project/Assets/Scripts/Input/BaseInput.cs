using UnityEngine;
using System.Collections;

// BaseInput is the base class for all inputs
// Polymorphism makes it so that weapons and movement can react to input
// set by the player, or by the AI
public class BaseInput : MonoBehaviour 
{
	#region Properties
	// Properties can be accessed by everyone
	// but only set by itself or derived classes
	public bool Fire
	{
		get;
		protected set;
	}

	public float TargetPosition
	{
		get;
		protected set;
	}
	#endregion
}
