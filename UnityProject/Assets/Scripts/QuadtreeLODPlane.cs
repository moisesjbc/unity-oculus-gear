using UnityEngine;
using System.Collections;

public class QuadtreeLODPlane : MonoBehaviour {
	QuadtreeLODNode rootNode = null;

	void Start () {
		Debug.Log ("Quadtree LOD plane created");
		Vector3 meshSize = GetComponent<MeshRenderer> ().bounds.size;
		if (meshSize.x != meshSize.z) {
			Debug.LogWarning ( "LOD plane must be square (currently: " + 
			                  meshSize.x + 
			                  "x" +
			                  meshSize.y +
			                  ")" );
		}

		float mapSize = Mathf.Max ( meshSize.x, meshSize.z );

		rootNode = new QuadtreeLODNode( mapSize, 20, transform, this.GetComponent<Material>() );
		GetComponent<MeshRenderer> ().enabled = false;
	}

	void Update () {
		rootNode.Update ();
	}
}
