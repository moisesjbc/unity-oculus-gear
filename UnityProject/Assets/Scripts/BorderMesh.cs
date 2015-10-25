using UnityEngine;
using System.Collections;

public class BorderMesh {
	
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
		float distanceBetweenOuterUV = 1.0f / (float)(nOuterVertices);
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

		GenerateFirstAndDeltaVertices (meshSize / 2.0f, distanceBetweenInnerVertices, borderPosition, out firstInnerVertex, out deltaInnerVertex);
		GenerateFirstAndDeltaUV (meshSize / 2.0f, distanceBetweenInnerUV, borderPosition, out firstInnerUV, out deltaInnerUV);

		for( int i=0; i<innerVertices.Length; i++ ){
			innerVertices[i] = firstInnerVertex + deltaInnerVertex * i;
			innerUV[i] = firstInnerUV + deltaInnerUV * i;
			Debug.Log ("innerUV[" + i + "]: " + innerUV[i]);
		}
	}


	public void GenerateFirstAndDeltaVertices( 
	                                 float meshHalfSize, 
	                                 float distanceBetweenVertices,
	                                 BorderPosition borderPosition,
	                                 out Vector3 firstVertex,
	                                 out Vector3 deltaVertex )
	{
		switch (borderPosition) {
			case BorderPosition.TOP:
				firstVertex = new Vector3 (
						-meshHalfSize + distanceBetweenVertices,
						0.0f,
						meshHalfSize - distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						distanceBetweenVertices,
						0.0f,
						0.0f
				);
				break;
			case BorderPosition.BOTTOM:
				firstVertex = new Vector3 (
						-meshHalfSize + distanceBetweenVertices,
						0.0f,
						-meshHalfSize + distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						distanceBetweenVertices,
						0.0f,
						0.0f
				);
				break;
			case BorderPosition.LEFT:
				firstVertex = new Vector3 (
						-meshHalfSize + distanceBetweenVertices,
						0.0f,
						meshHalfSize - distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						0.0f,
						0.0f,
						-distanceBetweenVertices
				);
				break;
			case BorderPosition.RIGHT:
				firstVertex = new Vector3 (
						meshHalfSize + distanceBetweenVertices,
						0.0f,
						meshHalfSize - distanceBetweenVertices
				);
				deltaVertex = new Vector3 (
						0.0f,
						0.0f,
						-distanceBetweenVertices
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
	                                    out Vector2 firstUV,
	                                    out Vector2 deltaUV )
	{
		switch (borderPosition) {
			case BorderPosition.TOP:
				firstUV = new Vector2(
					distanceBetweenUV,
					1.0f - distanceBetweenUV
					);
				deltaUV = new Vector2(
					distanceBetweenUV,
					0.0f
					);
			break;
			case BorderPosition.BOTTOM:
				firstUV = new Vector2(
					distanceBetweenUV,
					distanceBetweenUV
					);
				deltaUV = new Vector2(
					distanceBetweenUV,
					0.0f
					);
			break;
			case BorderPosition.LEFT:
				firstUV = new Vector2(
					distanceBetweenUV,
					distanceBetweenUV
					);
				deltaUV = new Vector2(
					0.0f,
					-distanceBetweenUV
					);
			break;
			case BorderPosition.RIGHT:
				firstUV = new Vector2(
					1.0f - distanceBetweenUV,
					1.0f - distanceBetweenUV
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
}
