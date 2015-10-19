using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	bool mouseLeftButtonPressed = false;
	float velocity = 1.0f;

	// Update is called once per frame
	void Update () {
		const float zoomStep = 1.0f;
		const float translationStep = 0.5f;

		// Allow user to zoom in and out with the mouse whell.
		float mouseZoom = Input.GetAxis ("Mouse ScrollWheel");
		transform.Translate ( mouseZoom * zoomStep * Vector3.forward);
		
		// Allow user to move over the map by moving the mouse.
		if (Input.GetMouseButtonDown (0)) {
			mouseLeftButtonPressed = true;
		} else if( Input.GetMouseButtonUp(0)) {
			mouseLeftButtonPressed = false;
		} else if (mouseLeftButtonPressed) {
			transform.Translate (Input.GetAxis ("Mouse Y") * -translationStep * Vector3.up);
			transform.Translate (Input.GetAxis ("Mouse X") * -translationStep * Vector3.right);
		}

		// Keep moving the player forward.
		transform.Translate (velocity * 0.001f * transform.position.y * Vector3.forward);
	}
}
