using UnityEngine;
using System.Collections;

public class TestCSharpConversor : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Angle a = Angle.fromDegrees(25);
		Angle b = a.times(4);
		GetComponent<TextMesh>().text = "Angle: " + b._degrees;
	}
}
