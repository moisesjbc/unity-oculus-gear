﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuadtreeLODPlane : MonoBehaviour{
	private QuadtreeLODNode rootNode = null;
	HeightMapsManager heightMapsManager = null;
	MapTexturesManager mapTexturesManager = null;


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

		if (rootNode != null) {
			GameObject[] mapSectors = GameObject.FindGameObjectsWithTag ("MapSector");
			for( int i=0; i<mapSectors.Length; i++ ){
				Destroy (mapSectors[i]);
			}
		}

		if (mapTexturesManager == null) {
			heightMapsManager = gameObject.AddComponent<HeightMapsManager> () as HeightMapsManager;
			mapTexturesManager = gameObject.AddComponent<MapTexturesManager> () as MapTexturesManager;
		}

		rootNode = new QuadtreeLODNode( 
		                               mapSize, 
		                               20, 
		                               bottomLeftCoordinates,
		                               topRightCoordinates,
		                               transform, 
		                               this.GetComponent<Material>(),
		                               mapTexturesManager,
		                               heightMapsManager
		                               );
		GetComponent<MeshRenderer> ().enabled = false;
	}


	void Update () {
		if (rootNode != null) {
			rootNode.Update ( false );
		}
	}


	public float GetHeight( Vector3 observer )
	{
		float height;
		if (rootNode != null && rootNode.ObserverOnSector (observer, out height)) {
			return height;
		} else {
			return observer.y;
		}
	}
}
