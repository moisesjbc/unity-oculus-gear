using UnityEngine;
using System.Collections;
using UnityEngine.VR;


public class CameraMovement : MonoBehaviour {
	bool mouseLeftButtonPressed = false;
	const float INITIAL_VELOCITY = 1.0f;
	float velocity = INITIAL_VELOCITY;
	Vector3 initialPosition = Vector3.zero;
	const float ROTATION_SENSITIVITY = 5.0f;
	const float MAX_VELOCITY = 3.0f;
	const float MIN_VELOCITY = -3.0f;


	void Awake(){
		initialPosition = transform.position;
		Cursor.lockState = CursorLockMode.Locked;
	}


	void LateUpdate () {
		const float NO_VR_MOVEMENT_HEIGHT_FACTOR = 0.05f;
		const float NO_VR_ZOOM_HEIGHT_FACTOR = 0.30f;
		const float VR_VELOCITY_HEIGHT_FACTOR = 0.001f;
		
		if (VRSettings.enabled) {
			// Increase or decrease velocity with Oculus touchpad.
			if (Input.GetMouseButtonDown (0)) {
				mouseLeftButtonPressed = true;
			} else if (Input.GetMouseButtonUp (0)) {
				mouseLeftButtonPressed = false;
			} else {
				if (Input.GetAxis ("Mouse X") > 0.0f) {
					velocity -= 1.0f;
					mouseLeftButtonPressed = false;
				} else if (Input.GetAxis ("Mouse X") < 0.0f) {
					velocity += 1.0f;
					mouseLeftButtonPressed = false;
				}
			}

			if (Input.GetMouseButtonDown (1)) {
				transform.position = initialPosition;
				velocity = INITIAL_VELOCITY;
			}
		} else {
			// Allow user to increase / decrease with the mouse wheel.
			if (Input.GetAxis ("Mouse ScrollWheel") > 0.0f) {
				velocity += 1.0f;
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0.0f) {
				velocity -= 1.0f;
			}
			
			// Allow user to rotate the camera with alt + the mouse.
			if( Input.GetKey(KeyCode.LeftAlt) ){
				transform.Rotate ( ROTATION_SENSITIVITY * -Input.GetAxis ("Mouse Y"),
				                  ROTATION_SENSITIVITY * Input.GetAxis ("Mouse X"), 
				                  0.0f );
			}
		}

		// Clamp velocity.
		velocity = Mathf.Clamp( velocity, MIN_VELOCITY, MAX_VELOCITY );

		// Move the player forward with the given velocity.
		transform.position += 
			GetComponent<OVRCameraRig> ().centerEyeAnchor.rotation * 
				(velocity * Vector3.forward);
	}
}
