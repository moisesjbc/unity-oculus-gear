using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	private Vector3 dstPoint;


	public void Awake()
	{
		// Set the destination point to the initial position of the 
		// VR camera.
		dstPoint = GameObject.Find ("CenterEyeAnchor").transform.position;
	}

	
	public void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.GetComponent<CameraMovement>() != null ){
			other.gameObject.GetComponent<CameraMovement>().Reset();
		}
	}
}
