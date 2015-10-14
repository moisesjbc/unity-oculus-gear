using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	private bool goingForward = true;
	public LODPlane lodPlane;

	// Use this for initialization
	void Start () {
		Instantiate (lodPlane,new Vector3(0,4,-5),Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		const float step = 0.015f;
		
		if( goingForward ){
			transform.Translate (step * Vector3.forward);
			if( transform.position.y < 5.0f ){
				goingForward = false;
			}
		}else{
			transform.Translate (-step * Vector3.forward);
			if( transform.position.y > 10.0f ){
				goingForward = true;
			}
		}
	}
}
