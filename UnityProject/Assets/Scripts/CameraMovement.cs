using UnityEngine;
using System.Collections;
using UnityEngine.VR;


public class CameraMovement : MonoBehaviour {
	bool mouseLeftButtonPressed = false;
	const float INITIAL_VELOCITY = 1.0f;
	float velocity = INITIAL_VELOCITY;
	Vector3 initialPosition = Vector3.zero;


	void Awake(){
		initialPosition = transform.position;
		Cursor.lockState = CursorLockMode.Locked;
	}


	void LateUpdate () {
		const float NO_VR_MOVEMENT_HEIGHT_FACTOR = 0.05f;
		const float NO_VR_ZOOM_HEIGHT_FACTOR = 0.30f;
		const float VR_VELOCITY_HEIGHT_FACTOR = 0.001f;
		
		if (VRSettings.enabled) {
			// Keep moving the player forward.
			transform.position += GetComponent<OVRCameraRig> ().centerEyeAnchor.rotation * (velocity * VR_VELOCITY_HEIGHT_FACTOR * transform.position.y * Vector3.forward);

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
			// Allow user to zoom in and out with the mouse whell.
			float mouseZoom = Input.GetAxis ("Mouse ScrollWheel");
			transform.Translate (mouseZoom * NO_VR_ZOOM_HEIGHT_FACTOR * transform.position.y * Vector3.forward);
			
			// Allow user to fly over the map by moving the mouse.
			if( Input.GetKey(KeyCode.LeftAlt) ){
				transform.Rotate ( 5.0f * -Input.GetAxis ("Mouse Y"),
				                  5.0f * Input.GetAxis ("Mouse X"), 
				                  0.0f );
			}
		}
	}
}
