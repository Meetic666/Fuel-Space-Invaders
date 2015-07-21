using UnityEngine;
using System.Collections;

public class PlayerInput : BaseInput 
{	
	// Update is called once per frame
	void Update () 
	{
		Fire = Input.GetMouseButton(0);
	}
}
