using UnityEngine;
using System.Collections;

public class PickupCollection : MonoBehaviour {

	private int score = 0;


	public void Reset()
	{
		score = 0;
	}


	public int Score()
	{
		return score;
	}


	public void OnTriggerEnter( Collider other )
	{
		if (other.gameObject.tag == "Pickup") {
			gameObject.GetComponent<AudioSource>().Play ();
			Destroy (other.transform.parent.gameObject);
			score++;
			Debug.Log ("Pickup!");
		}
	}
}
