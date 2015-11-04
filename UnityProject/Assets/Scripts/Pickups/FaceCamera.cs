using UnityEngine;
using System.Collections;

public class FaceCamera : MonoBehaviour {
	
	void Update () 
	{
		gameObject.transform.LookAt( Camera.main.transform.position );
		gameObject.transform.Rotate ( 0.0f, 180.0f, 0.0f );
	}

}
