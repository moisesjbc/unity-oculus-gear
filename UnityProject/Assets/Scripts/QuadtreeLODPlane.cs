﻿using UnityEngine;
using System.Collections;

public class QuadtreeLODPlane : MonoBehaviour {
	QuadtreeLODNode rootNode = null;

	void Start () {
		Debug.Log ("Quadtree LOD plane created");
		rootNode = new QuadtreeLODNode( 20, transform, this.GetComponent<Material>() );
		GetComponent<MeshRenderer> ().enabled = false;
	}

	void Update () {
		rootNode.Update ();
	}
}
