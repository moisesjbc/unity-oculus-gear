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

		Vector2 bottomLeftCoordinates = new Vector2 ( 416000,3067000 );
		Vector2 topRightCoordinates = new Vector2 ( 466000,3117000 );

		rootNode = new QuadtreeLODNode( 
		                               mapSize, 
		                               20, 
		                               bottomLeftCoordinates,
		                               topRightCoordinates,
		                               transform, 
		                               this.GetComponent<Material>() 
		                               );
		GetComponent<MeshRenderer> ().enabled = false;
	}

	void Update () {
		rootNode.Update ();
	}
}
