using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class LODPlane : MonoBehaviour {
	private Texture2D[] textures;
	private Mesh[] meshes;

	// Use this for initialization
	void Start () {
		Debug.Log ("LOD Plane created");
		
		GetComponent<Renderer>().material.shader = Shader.Find("Sprites/Default");

		textures = new Texture2D[3];
		for( int i = 0; i < textures.Length; i++) {
			textures[i] = Texture2D.whiteTexture;
		}

		StartCoroutine (LoadImage ("http://pixelkin.org/wp-content/uploads/2014/03/Metal-Gear-Solid-Color-Logo.jpg", 0));
		StartCoroutine (LoadImage ("https://upload.wikimedia.org/wikipedia/commons/7/7e/Metal_Gear_Solid_2_logo.png", 1));
		StartCoroutine (LoadImage ("https://upload.wikimedia.org/wikipedia/commons/c/c0/Metal_Gear_Solid_3_logo.png", 2));

		meshes = new Mesh[3];
		meshes [0] = GetComponent<MeshFilter> ().mesh;
		for (int i=1; i<3; i++) {
			meshes[i] = SubdivideMesh (meshes[i-1]);
		}		 
	}
	
	// Update is called once per frame
	void Update () {
		float distance = 
			Vector3.Distance (transform.position, Camera.main.transform.position);
		
		if (distance < 9.0f) {
			GetComponent<Renderer>().material.mainTexture = textures[2];
			GetComponent<MeshFilter> ().mesh = meshes[2];
		} else if (distance < 10.0f) {
			GetComponent<Renderer>().material.mainTexture = textures[1];
			GetComponent<MeshFilter> ().mesh = meshes[1];
		} else {
			GetComponent<Renderer>().material.mainTexture = textures[0];
			GetComponent<MeshFilter> ().mesh = meshes[0];
		}
	}

	private IEnumerator LoadImage( string url, int textureIndex ) {
		WWW www = new WWW( url );
		yield return www;
		textures[textureIndex] = www.texture;
	}


	Mesh SubdivideMesh(Mesh srcMesh)
	{		
		var vertices = srcMesh.vertices.ToList ();
		var uv = srcMesh.uv.ToList ();
		List<int> indices = new List<int>();

		for (int triangleFirstVertexIndex = 0; triangleFirstVertexIndex < srcMesh.triangles.Length; triangleFirstVertexIndex += 3) {
			// Retrieve all the indices of the plane to be subdivided.
			var triangleVertexIndices = new int[]
			{
				srcMesh.triangles [triangleFirstVertexIndex],
				srcMesh.triangles [triangleFirstVertexIndex + 1],
				srcMesh.triangles [triangleFirstVertexIndex + 2]
			};

			var triangleVertices = new Vector3[]
			{
				vertices [triangleVertexIndices [0]],
				vertices [triangleVertexIndices [1]],
				vertices [triangleVertexIndices [2]],
			};

			var triangleUV = new Vector2[]
			{
				uv [triangleVertexIndices [0]],
				uv [triangleVertexIndices [1]],
				uv [triangleVertexIndices [2]]
			};
			
			// Compute the middle vertices of the plane and push them into the vertices vector.
			int[] midleVertexIndices = new int[4];
			for (int i = 0; i < 3; i++) {
				Vector3 currentVertex = triangleVertices [i];
				Vector2 currentUV = triangleUV [i];
				Vector3 nextVertex = triangleVertices [(i + 1) % 3];
				Vector2 nextUV = triangleUV [(i + 1) % 3];
				
				Vector3 middleVertexPos = new Vector3 (
					(currentVertex.x + nextVertex.x) / 2.0f,
					(currentVertex.y + nextVertex.y) / 2.0f,
					(currentVertex.z + nextVertex.z) / 2.0f
				);
				
				Vector2 middleVertexUV = new Vector2 (
					(currentUV.x + nextUV.x) / 2.0f,
					(currentUV.y + nextUV.y) / 2.0f
				);

				vertices.Add (middleVertexPos);
				uv.Add (middleVertexUV);				
				midleVertexIndices [i] = vertices.Count - 1;
			}

			// Subtriangle 0
			indices.Add (triangleVertexIndices[0]);
			indices.Add (midleVertexIndices[0]);
			indices.Add (midleVertexIndices[2]);

			// Subtriangle 1
			indices.Add (midleVertexIndices[0]);
			indices.Add (triangleVertexIndices[1]);
			indices.Add (midleVertexIndices[1]);

			// Subtriangle 2
			indices.Add (midleVertexIndices[2]);
			indices.Add (midleVertexIndices[0]);
			indices.Add (midleVertexIndices[1]);
			
			// Subtriangle 3
			indices.Add (midleVertexIndices[2]);
			indices.Add (midleVertexIndices[1]);
			indices.Add (triangleVertexIndices[2]);
		}
		
		Mesh dstMesh = new Mesh ();
		dstMesh.vertices = vertices.ToArray ();
		dstMesh.uv = uv.ToArray ();
		dstMesh.triangles = indices.ToArray ();
		dstMesh.RecalculateBounds ();
		dstMesh.RecalculateNormals ();

		return dstMesh;
	}
}
