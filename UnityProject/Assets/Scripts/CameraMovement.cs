using UnityEngine;
using System.Collections;
using UnityEngine.VR;


public class CameraMovement : MonoBehaviour {
	bool mouseLeftButtonPressed = false;
	const float INITIAL_SPEED = 0.0f;
	float speed = INITIAL_SPEED;
	Vector3 initialPosition = Vector3.zero;
	const float ROTATION_SENSITIVITY = 5.0f;
	const float MAX_SPEED = 5.0f;
	const float MIN_SPEED = -5.0f;
	const float SPEED_STEP = 0.5f;


	void Awake(){
		initialPosition = transform.position;
		Cursor.lockState = CursorLockMode.Locked;
		GetComponent<Rigidbody> ().freezeRotation = true;
	}


	void FixedUpdate () {
		const float NO_VR_MOVEMENT_HEIGHT_FACTOR = 0.05f;
		const float NO_VR_ZOOM_HEIGHT_FACTOR = 0.30f;
		const float VR_VELOCITY_HEIGHT_FACTOR = 0.001f;
		
		if (VRSettings.enabled) {
			// Increase or decrease speed with Oculus touchpad.
			if (Input.GetMouseButtonDown (0)) {
				mouseLeftButtonPressed = true;
			} else if (Input.GetMouseButtonUp (0)) {
				mouseLeftButtonPressed = false;
			} else if( mouseLeftButtonPressed ){
				if (Input.GetAxis ("Mouse X") > 0.0f) {
					speed -= SPEED_STEP;
					mouseLeftButtonPressed = false;
				} else if (Input.GetAxis ("Mouse X") < 0.0f) {
					speed += SPEED_STEP;
					mouseLeftButtonPressed = false;
				}
			}

			if (Input.GetMouseButtonDown (1)) {
				transform.position = initialPosition;
				speed = INITIAL_SPEED;
			}
		} else {
			// Allow user to increase / decrease speed with the mouse wheel.
			if (Input.GetAxis ("Mouse ScrollWheel") > 0.0f) {
				speed += SPEED_STEP;
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0.0f) {
				speed -= SPEED_STEP;
			}
			
			// Allow user to rotate the camera with alt + the mouse.
			if( Input.GetKey(KeyCode.LeftAlt) ){
				transform.Rotate ( ROTATION_SENSITIVITY * -Input.GetAxis ("Mouse Y"),
				                  ROTATION_SENSITIVITY * Input.GetAxis ("Mouse X"), 
				                  0.0f );
			}
		}

		// Clamp speed.
		speed = Mathf.Clamp( speed, MIN_SPEED, MAX_SPEED );

		// Move the player forward with the given speed.
		GetComponent<Rigidbody>().MovePosition(transform.position + GetComponent<OVRCameraRig> ().centerEyeAnchor.rotation * 
		                                       (speed * Time.fixedDeltaTime * Vector3.forward));
	}


	void OnCollisionEnter()
	{
		speed = 0.0f;
	}
}
