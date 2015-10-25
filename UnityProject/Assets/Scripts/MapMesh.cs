using UnityEngine;
using System.Collections;

public class MapMesh {

	private Mesh innerMesh;
	private BorderMesh[] borderMeshes_;


	public MapMesh( float meshSize, int meshVertexResolution )
	{
		int N_VERTICES = meshVertexResolution * meshVertexResolution;
		float DISTANCE_BETWEEN_VERTICES = meshSize / (float)(meshVertexResolution - 1.0f) ;
		float DISTANCE_BETWEEN_UV = 1.0f / (float)meshVertexResolution;

		Vector3[] vertices = new Vector3[N_VERTICES];
		Vector2[] uv = new Vector2[N_VERTICES];

		// Generate vertices and UV.
		for (int row=0; row<meshVertexResolution; row++) {
			for (int column=0; column<meshVertexResolution; column++) {
				int VERTEX_INDEX = row * meshVertexResolution + column;

				vertices[VERTEX_INDEX].x = -meshSize / 2.0f + column * DISTANCE_BETWEEN_VERTICES;
				vertices[VERTEX_INDEX].y = 0.0f;
				vertices[VERTEX_INDEX].z = meshSize / 2.0f - row * DISTANCE_BETWEEN_VERTICES;

				uv[VERTEX_INDEX].x = DISTANCE_BETWEEN_UV * column;
				uv[VERTEX_INDEX].y = 1.0f - DISTANCE_BETWEEN_UV * row;
			}
		}

		// Generate triangles
		int N_TRIANGLES = 2 * (meshVertexResolution - 1) * (meshVertexResolution - 1);
		int[] triangles = new int[N_TRIANGLES * 3];
		int triangleIndex = 0;
		for (int row=0; row<meshVertexResolution - 1; row++) {
			for (int column=0; column<meshVertexResolution - 1; column++) {
				triangles[triangleIndex] = GetVertexIndex( row, column, meshVertexResolution );
				triangles[triangleIndex + 1] = GetVertexIndex( row, column+1, meshVertexResolution ); 
				triangles[triangleIndex + 2] = GetVertexIndex( row+1, column, meshVertexResolution ); 

				triangles[triangleIndex + 3] = GetVertexIndex( row, column+1, meshVertexResolution );
				triangles[triangleIndex + 4] = GetVertexIndex( row+1, column+1, meshVertexResolution ); 
				triangles[triangleIndex + 5] = GetVertexIndex( row+1, column, meshVertexResolution ); 

				triangleIndex += 6;
			}
		}

		innerMesh = new Mesh ();
		innerMesh.vertices = vertices;
		innerMesh.triangles = triangles;
		innerMesh.uv = uv;
		innerMesh.RecalculateNormals ();
		innerMesh.RecalculateBounds ();

		borderMeshes_ = new BorderMesh[4]{
			new BorderMesh (meshSize, meshVertexResolution, 0, BorderMesh.BorderPosition.TOP),
			new BorderMesh (meshSize, meshVertexResolution, 0, BorderMesh.BorderPosition.BOTTOM),
			new BorderMesh (meshSize, meshVertexResolution, 0, BorderMesh.BorderPosition.LEFT),
			new BorderMesh (meshSize, meshVertexResolution, 0, BorderMesh.BorderPosition.RIGHT)
		};
	}


	public MapMesh( MapMesh srcMesh )
	{
		innerMesh = new Mesh ();
		innerMesh.vertices = srcMesh.innerMesh.vertices;
		innerMesh.triangles = srcMesh.innerMesh.triangles;
		innerMesh.uv = srcMesh.innerMesh.uv;
		innerMesh.RecalculateNormals ();
		innerMesh.RecalculateBounds ();

		borderMeshes_ = srcMesh.borderMeshes_;
	}


	static private int GetVertexIndex( int row, int column, int verticesPerRow )
	{
		return row * verticesPerRow + column;
	}


	public Vector3 GetSize()
	{
		return innerMesh.bounds.size;
	}


	public void SetHeightsMap( float[,] heights )
	{
		Vector3[] vertices = innerMesh.vertices;
		
		int N_ROWS = heights.GetLength(0);
		for (int row=0; row<N_ROWS; row++) {
			// FIXME: This is forcing N_COLUMS = N_ROWS.
			int N_COLUMNS = N_ROWS;
			for (int column=0; column<N_COLUMNS; column++) {
				int VERTEX_INDEX = row * N_COLUMNS + column;
				vertices[VERTEX_INDEX].y = heights[row,column] / 1000.0f; /// maxHeight;
			}
		}
		
		innerMesh.vertices = vertices;
		innerMesh.RecalculateBounds ();
		innerMesh.RecalculateNormals ();
	}


	public void Render( Matrix4x4 modelMatrix, Material material, int layer )
	{
		Graphics.DrawMesh (innerMesh, modelMatrix, material, layer);
	}
}
