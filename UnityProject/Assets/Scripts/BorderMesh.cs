using UnityEngine;
using System.Collections;

public class BorderMesh {

	Mesh mesh_;
	
	public enum BorderPosition
	{
		TOP,
		BOTTOM,
		LEFT,
		RIGHT
	};


	public BorderMesh( float meshSize, int meshVertexResolution, int depth, BorderPosition borderPosition )
	{
		int nInnerVertices = meshVertexResolution - 2;
		int nOuterVertices = nInnerVertices * (depth + 1);

		float distanceBetweenInnerVertices = meshSize / (float)(meshVertexResolution - 1);
		float distanceBetweenInnerUV = 1.0f / (float)(meshVertexResolution);
		Vector3[] innerVertices = new Vector3[nInnerVertices];
		Vector2[] innerUV = new Vector2[nInnerVertices];

		float distanceBetweenOuterVertices = meshSize / (float)(nOuterVertices - 1);
		float distanceBetweenOuterUV = 1.0f / (float)(nOuterVertices - 1);
		Vector3[] outerVertices = new Vector3[nOuterVertices];
		Vector2[] outerUV = new Vector2[nOuterVertices];

		Vector3 firstInnerVertex = Vector3.zero;
		Vector3 deltaInnerVertex = Vector3.zero;

		Vector2 firstInnerUV = Vector3.zero;
		Vector2 deltaInnerUV = Vector3.zero;
		
		Vector3 firstOuterVertex = Vector3.zero;
		Vector3 deltaOuterVertex = Vector3.zero;
		
		Vector2 firstOuterUV = Vector3.zero;
		Vector2 deltaOuterUV = Vector3.zero;

		GenerateFirstAndDeltaVertices (meshSize / 2.0f, distanceBetweenInnerVertices, borderPosition, true, out firstInnerVertex, out deltaInnerVertex);
		GenerateFirstAndDeltaUV (meshSize / 2.0f, distanceBetweenInnerUV, borderPosition, true, out firstInnerUV, out deltaInnerUV);
		GenerateFirstAndDeltaVertices (meshSize / 2.0f, distanceBetweenOuterVertices, borderPosition, false, out firstOuterVertex, out deltaOuterVertex);
		GenerateFirstAndDeltaUV (meshSize / 2.0f, distanceBetweenOuterUV, borderPosition, false, out firstOuterUV, out deltaOuterUV);

		for( int i=0; i<innerVertices.Length; i++ ){
			innerVertices[i] = firstInnerVertex + deltaInnerVertex * i;
			innerUV[i] = firstInnerUV + deltaInnerUV * i;
			Debug.Log ("innerUV[" + i + "]: " + innerUV[i]);
		}

		for( int i=0; i<outerVertices.Length; i++ ){
			outerVertices[i] = firstOuterVertex + deltaOuterVertex * i;
			outerUV[i] = firstOuterUV + deltaOuterUV * i;
			Debug.Log ("outerUV[" + i + "]: " + outerUV[i]);
		}
			
		mesh_ = new Mesh();

		System.Array.Copy (innerVertices, mesh_.vertices, innerVertices.Length);
		System.Array.Copy (outerVertices, 0, mesh_.vertices, innerVertices.Length, outerVertices.Length);

		System.Array.Copy (innerUV, mesh_.uv, innerUV.Length);
		System.Array.Copy (outerUV, 0, mesh_.uv, innerUV.Length, outerUV.Length);

		mesh_.triangles = GenerateTriangles (nInnerVertices, nOuterVertices);
		mesh_.RecalculateBounds();
		mesh_.RecalculateNormals();
	}


	public void GenerateFirstAndDeltaVertices( 
	                                 float meshHalfSize, 
	                                 float distanceBetweenVertices,
	                                 BorderPosition borderPosition,
	                                 bool innerBorder,
	                                 out Vector3 firstVertex,
	                                 out Vector3 deltaVertex )
	{
		float innerBorderFactor = (innerBorder) ? 1.0f : 0.0f;

		switch (borderPosition) {
			case BorderPosition.TOP:
				firstVertex = new Vector3 (
						-meshHalfSize + innerBorderFactor * distanceBetweenVertices,
						0.0f,
						meshHalfSize - innerBorderFactor * distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						distanceBetweenVertices,
						0.0f,
						0.0f
				);
				break;
			case BorderPosition.BOTTOM:
				firstVertex = new Vector3 (
						-meshHalfSize + innerBorderFactor * distanceBetweenVertices,
						0.0f,
						-meshHalfSize + innerBorderFactor * distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						distanceBetweenVertices,
						0.0f,
						0.0f
				);
				break;
			case BorderPosition.LEFT:
				firstVertex = new Vector3 (
						-meshHalfSize + innerBorderFactor * distanceBetweenVertices,
						0.0f,
						meshHalfSize - innerBorderFactor * distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						0.0f,
						0.0f,
						distanceBetweenVertices
				);
				break;
			case BorderPosition.RIGHT:
				firstVertex = new Vector3 (
						meshHalfSize + innerBorderFactor * distanceBetweenVertices,
						0.0f,
						meshHalfSize - innerBorderFactor * distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						0.0f,
						0.0f,
						distanceBetweenVertices
				);
				break;
			default:
				firstVertex = Vector3.zero;
				deltaVertex = Vector3.zero;
				Debug.LogError ("Bad BorderPosition specified in constructor");
				break;
		}
	}


	public void GenerateFirstAndDeltaUV( 
	                                    float meshHalfSize, 
	                                    float distanceBetweenUV,
	                                    BorderPosition borderPosition,
	                                    bool innerBorder,
	                                    out Vector2 firstUV,
	                                    out Vector2 deltaUV )
	{
		float innerBorderFactor = (innerBorder) ? 1.0f : 0.0f;

		switch (borderPosition) {
			case BorderPosition.TOP:
				firstUV = new Vector2(
					innerBorderFactor * distanceBetweenUV,
					1.0f - innerBorderFactor * distanceBetweenUV
					);
				deltaUV = new Vector2(
					distanceBetweenUV,
					0.0f
					);
			break;
			case BorderPosition.BOTTOM:
				firstUV = new Vector2(
					innerBorderFactor * distanceBetweenUV,
					innerBorderFactor * distanceBetweenUV
					);
				deltaUV = new Vector2(
					distanceBetweenUV,
					0.0f
					);
			break;
			case BorderPosition.LEFT:
				firstUV = new Vector2(
					innerBorderFactor * distanceBetweenUV,
					1.0f - innerBorderFactor * distanceBetweenUV
					);
				deltaUV = new Vector2(
					0.0f,
					- distanceBetweenUV
					);
			break;
			case BorderPosition.RIGHT:
				firstUV = new Vector2(
					1.0f - innerBorderFactor * distanceBetweenUV,
					1.0f - innerBorderFactor * distanceBetweenUV
					);
				deltaUV = new Vector2(
					0.0f,
					-distanceBetweenUV
					);
			break;
			default:
				firstUV = Vector2.zero;
				deltaUV = Vector2.zero;
				Debug.LogError ("Bad BorderPosition specified in constructor");
			break;
		}
	}


	public int[] GenerateTriangles( int nInnerVertices, int nOuterVertices )
	{
		int nVertices = nInnerVertices + nOuterVertices;
		int nTriangles = 2 * (nVertices - 1) * (nVertices - 1);
		int[] triangles = new int[nTriangles * 3];
		int trianglesPerInnerVertex = nOuterVertices / nInnerVertices;

		int triangleIndex = 0;
		for (int innerVertexIndex = 0; innerVertexIndex < nInnerVertices; innerVertexIndex++) {
			int outerVertexIndex = innerVertexIndex + nInnerVertices;
			for(int i = 0; i < trianglesPerInnerVertex; i++ ){
				triangles[triangleIndex] = innerVertexIndex;
				triangles[triangleIndex+1] = outerVertexIndex;
				triangles[triangleIndex+2] = outerVertexIndex + 1;

				outerVertexIndex++;
				triangleIndex += 3;
			}
		}

		return triangles;
	}
}
