using UnityEngine;
using System.Collections;

public class PickupCollection : MonoBehaviour {

	void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.tag == "Pickup") {
			Destroy (other.gameObject);
			Debug.Log ("Pickup!");
		}
	}
}
