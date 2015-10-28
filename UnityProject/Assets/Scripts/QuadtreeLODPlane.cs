using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadtreeLODPlane : MonoBehaviour {
	QuadtreeLODNode rootNode = null;

	public enum Island {
		GRAN_CANARIA,
		TENERIFE,
		LA_PALMA
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

		List<Vector3> pickupPositions = new List<Vector3>();

		switch (island) {
			case Island.GRAN_CANARIA:
				bottomLeftCoordinates = new Vector2 ( 416000,3067000 );
				topRightCoordinates = new Vector2 ( 466000,3117000 );
				
				pickupPositions.Add( new Vector3( 0f, 3f, -15f ) );
				pickupPositions.Add( new Vector3( -3.6f, 5.2f, 5f ) );
			break;
			case Island.TENERIFE:
				bottomLeftCoordinates = new Vector2 ( 310000,3090000 );
				topRightCoordinates = new Vector2 ( 392000,3172000 );
			break;
			case Island.LA_PALMA:
				bottomLeftCoordinates = new Vector2 ( 192500,3145000 );
				topRightCoordinates = new Vector2 ( 247500,3200000 );
			break;
		}

		foreach( Vector3 pickupPosition in pickupPositions )
		{
			Instantiate( Resources.Load("Pickup"), pickupPosition, Quaternion.identity );
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
