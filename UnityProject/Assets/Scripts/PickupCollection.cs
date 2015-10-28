﻿using UnityEngine;
using System.Collections;

public class PickupCollection : MonoBehaviour {

	public int score = 0;

	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.tag == "Pickup") {
			Destroy (other.gameObject);
			score++;
			Debug.Log ("Pickup!");
		}
	}
}
