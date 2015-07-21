using UnityEngine;
using System.Collections;

public class BaseInput : MonoBehaviour 
{
	public bool Fire
	{
		get;
		protected set;
	}

	public int MoveDirection
	{
		get;
		protected set;
	}
}
