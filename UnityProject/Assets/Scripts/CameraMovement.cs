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
	}


	void LateUpdate () {
		const float zoomStep = 1.0f;
		const float translationStep = 0.5f;
		
		if (VRSettings.enabled) {
			// Keep moving the player forward.
			transform.position += GetComponent<OVRCameraRig> ().centerEyeAnchor.rotation * (velocity * 0.001f * transform.position.y * Vector3.forward);

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
			transform.Translate (mouseZoom * zoomStep * Vector3.forward);
			
			// Allow user to move over the map by moving the mouse.
			if (Input.GetMouseButtonDown (0)) {
				mouseLeftButtonPressed = true;
			} else if (Input.GetMouseButtonUp (0)) {
				mouseLeftButtonPressed = false;
			} else if (mouseLeftButtonPressed) {
				transform.Translate (Input.GetAxis ("Mouse Y") * -translationStep * Vector3.up);
				transform.Translate (Input.GetAxis ("Mouse X") * -translationStep * Vector3.right);
			}
		}
	}
}
