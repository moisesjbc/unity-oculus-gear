using UnityEngine;
using System.Collections;

public class QuadtreeLODPlane : MonoBehaviour {
	QuadtreeLODNode rootNode = null;

	public void Reset( Vector2 bottomLeftCoordinates, Vector3 topRightCoordinates )
	{
		Vector3 meshSize = GetComponent<MeshRenderer> ().bounds.size;
		if (meshSize.x != meshSize.z) {
			Debug.LogWarning ("LOD plane must be square (currently: " + 
			                  meshSize.x + 
			                  "x" +
			                  meshSize.y +
			                  ")");
		}

		float mapSize = Mathf.Max ( meshSize.x, meshSize.z );
		
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
		if (rootNode != null) {
			rootNode.Update ();
		}
	}
}
