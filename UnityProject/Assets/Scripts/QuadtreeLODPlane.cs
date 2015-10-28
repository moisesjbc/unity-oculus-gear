using UnityEngine;
using System.Collections;

public class QuadtreeLODPlane : MonoBehaviour {
	QuadtreeLODNode rootNode = null;

	public enum Island {
		GRAN_CANARIA,
		TENERIFE
	};

	public Island island;

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

		Vector2 bottomLeftCoordinates = Vector2.zero;
		Vector2 topRightCoordinates = Vector2.zero;

		switch (island) {
			case Island.GRAN_CANARIA:
				bottomLeftCoordinates = new Vector2 ( 416000,3067000 );
				topRightCoordinates = new Vector2 ( 466000,3117000 );
			break;
			case Island.TENERIFE:
				bottomLeftCoordinates = new Vector2 ( 310000,3090000 );
				topRightCoordinates = new Vector2 ( 392000,3172000 );
			break;
		}

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
