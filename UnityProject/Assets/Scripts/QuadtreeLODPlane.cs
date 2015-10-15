using UnityEngine;
using System.Collections;

public class QuadtreeLODPlane : MonoBehaviour {
	QuadtreeLODNode rootNode = null;

	void Start () {
		Debug.Log ("Quadtree LOD plane created");
		rootNode = new QuadtreeLODNode( this.GetComponent<MeshFilter>().mesh, transform, this.GetComponent<Material>() );
		GetComponent<MeshRenderer> ().enabled = false;
	}

	void Update () {
		rootNode.Update ();
		rootNode.Render ();
	}
}
