using UnityEngine;
using System.Collections;

public class AutomaticRotation : MonoBehaviour {
	
	void Update () 
	{
		transform.Rotate (0.0f, 75.0f * Time.deltaTime, 0.0f);
	}
}
