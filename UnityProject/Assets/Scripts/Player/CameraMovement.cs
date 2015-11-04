﻿using UnityEngine;
using System.Collections;
using UnityEngine.VR;


public class CameraMovement : MonoBehaviour {
	bool mouseLeftButtonPressed = false;
	public const float INITIAL_SPEED = 0.0f;
	float speed = INITIAL_SPEED;
	Vector3 initialPosition = Vector3.zero;
	const float ROTATION_SENSITIVITY = 5.0f;
	public float MAX_SPEED = 5.0f;
	public float MIN_SPEED = -5.0f;
	public float SPEED_STEP = 0.5f;
	public bool SPEED_DEPENDS_ON_HEIGHT = true;
	public GameObject mapPlane;
	Vector3 velocity;


	void Awake(){
		initialPosition = transform.position;
		Cursor.lockState = CursorLockMode.Locked;
		GetComponent<Rigidbody> ().freezeRotation = true;
	}


	public void Reset()
	{
		transform.position = initialPosition;
		UnityEngine.VR.InputTracking.Recenter ();
	}


	void ModifySpeed( float delta )
	{
		speed += delta;

		// Clamp speed.
		speed = Mathf.Clamp( speed, MIN_SPEED, MAX_SPEED );
	}


	void FixedUpdate () {
		const float SPEED_HEIGHT_FACTOR = 0.3f;
		
		if (VRSettings.enabled) {
			// Increase or decrease speed with Oculus touchpad.
			if (Input.GetMouseButtonDown (0)) {
				mouseLeftButtonPressed = true;
			} else if (Input.GetMouseButtonUp (0)) {
				mouseLeftButtonPressed = false;
			} else if( mouseLeftButtonPressed ){
				if (Input.GetAxis ("Mouse X") > 0.0f) {
					ModifySpeed( -SPEED_STEP );
					mouseLeftButtonPressed = false;
				} else if (Input.GetAxis ("Mouse X") < 0.0f) {
					ModifySpeed( +SPEED_STEP );
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
				ModifySpeed( +SPEED_STEP );
			} else if (Input.GetAxis ("Mouse ScrollWheel") < 0.0f) {
				ModifySpeed( -SPEED_STEP );
			}
			
			// Allow user to rotate the camera with alt + the mouse.
			if( Input.GetKey(KeyCode.LeftAlt) ){
				transform.Rotate ( ROTATION_SENSITIVITY * -Input.GetAxis ("Mouse Y"),
				                  ROTATION_SENSITIVITY * Input.GetAxis ("Mouse X"), 
				                  0.0f );
			}
		}

		
		// Compute velocity vector.
		velocity = GetComponent<OVRCameraRig> ().centerEyeAnchor.rotation * (speed * Vector3.forward);

		if (SPEED_DEPENDS_ON_HEIGHT) {
			float height = mapPlane.GetComponent<QuadtreeLODPlane> ().GetHeight (transform.position);
			velocity *= SPEED_HEIGHT_FACTOR * Mathf.Max ( height, 1.5f );
		}

		// Move the player forward with the given speed.
		GetComponent<Rigidbody> ().MovePosition( transform.position + velocity * Time.fixedDeltaTime );
	}


	void OnCollisionEnter()
	{
		speed = 0.0f;
	}
}
