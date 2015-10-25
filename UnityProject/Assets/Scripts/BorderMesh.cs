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


	public BorderMesh( float meshSize, int meshVertexResolution, BorderPosition borderPosition )
	{
		int nInnerVertices = meshVertexResolution - 2;
		float distanceBetweenVertices = meshSize / (float)(meshVertexResolution - 1);
		float distanceBetweenUV = 1.0f / (float)(meshVertexResolution);
		Vector3[] innerVertices = new Vector3[nInnerVertices];
		Vector2[] innerUV = new Vector2[nInnerVertices];

		Vector3 firstInnerVertex = Vector3.zero;
		Vector3 deltaInnerVertex = Vector3.zero;

		Vector2 firstInnerUV = Vector3.zero;
		Vector2 deltaInnerUV = Vector3.zero;

		switch (borderPosition) {
			case BorderPosition.TOP:
				firstInnerVertex = new Vector3(
					-meshSize / 2.0f + distanceBetweenVertices,
					0.0f,
					meshSize / 2.0f - distanceBetweenVertices
				);
				deltaInnerVertex = new Vector3(
				distanceBetweenVertices,
					0.0f,
					0.0f
				);
				firstInnerUV = new Vector2(
					distanceBetweenUV,
					1.0f - distanceBetweenUV
				);
				deltaInnerUV = new Vector2(
					distanceBetweenUV,
					0.0f
				);
			break;
			case BorderPosition.BOTTOM:
				firstInnerVertex = new Vector3(
					-meshSize / 2.0f + distanceBetweenVertices,
					0.0f,
					-meshSize / 2.0f + distanceBetweenVertices
					);
				deltaInnerVertex = new Vector3(
					distanceBetweenVertices,
					0.0f,
					0.0f
					);
				firstInnerUV = new Vector2(
					distanceBetweenUV,
					distanceBetweenUV
					);
				deltaInnerUV = new Vector2(
					distanceBetweenUV,
					0.0f
					);
			break;
			case BorderPosition.LEFT:
				firstInnerVertex = new Vector3(
					-meshSize / 2.0f + distanceBetweenVertices,
					0.0f,
					meshSize / 2.0f - distanceBetweenVertices
					);
				deltaInnerVertex = new Vector3(
					0.0f,
					0.0f,
					-distanceBetweenVertices
					);
				firstInnerUV = new Vector2(
					distanceBetweenUV,
					distanceBetweenUV
					);
				deltaInnerUV = new Vector2(
					0.0f,
					-distanceBetweenUV
					);
			break;
			case BorderPosition.RIGHT:
				firstInnerVertex = new Vector3(
					meshSize / 2.0f + distanceBetweenVertices,
					0.0f,
					meshSize / 2.0f - distanceBetweenVertices
					);
				deltaInnerVertex = new Vector3(
					0.0f,
					0.0f,
					-distanceBetweenVertices
					);
				firstInnerUV = new Vector2(
					1.0f - distanceBetweenUV,
					1.0f - distanceBetweenUV
					);
				deltaInnerUV = new Vector2(
					0.0f,
					-distanceBetweenUV
					);
			break;
			default:
				Debug.LogError ( "Bad BorderPosition specified in constructor" );
			break;
		}

		for( int i=0; i<innerVertices.Length; i++ ){
			innerVertices[i] = firstInnerVertex + deltaInnerVertex * i;
			innerUV[i] = firstInnerUV + deltaInnerUV * i;
			//Debug.Log ("innerUV[" + i + "]: " + innerUV[i]);
		}
	}
}
