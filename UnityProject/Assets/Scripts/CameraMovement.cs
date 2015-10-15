using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	bool mouseLeftButtonPressed = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		const float zoomStep = 1.0f;
		const float translationStep = 0.5f;

		// Allow user to zoom in and out with the mouse whell.
		float mouseZoom = Input.GetAxis ("Mouse ScrollWheel");
		transform.Translate ( mouseZoom * zoomStep * Vector3.forward);

		if (Input.GetMouseButtonDown (0)) {
			mouseLeftButtonPressed = true;
		} else if( Input.GetMouseButtonUp(0)) {
			mouseLeftButtonPressed = false;
		}

		// Allow user to move over the map by moving the mouse.
		if (mouseLeftButtonPressed) {
			transform.Translate (Input.GetAxis ("Mouse Y") * -translationStep * Vector3.up);
			transform.Translate (Input.GetAxis ("Mouse X") * -translationStep * Vector3.right);
		}
	}
}
