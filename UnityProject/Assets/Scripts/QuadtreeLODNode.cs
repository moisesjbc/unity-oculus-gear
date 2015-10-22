using UnityEngine;
using System.Collections;

public class QuadtreeLODNode {
	// FIXME: This DumbGameObject is required for making children 
	// transforms relative to parent ones. Find a workaround so
	// this isn't necessary.
	private GameObject dumbGameObject_;
	private Transform transform_;
	private Mesh mesh_;
	private Material material_;
	private bool visible_;
	Vector2 bottomLeftCoordinates_;
	Vector2 topRightCoordinates_;
	
	QuadtreeLODNode[] children_;

	private int depth_ = 0;
	const int MAX_DEPTH = 7;

	const float THRESHOLD_FACTOR = 2.5f;

	WWW wwwService_ = null;
	bool textureLoaded = false;

	WWW heightMapRequest = null;
	bool heightMapLoaded = false;

	WWW childrenHeightMapRequest = null;
	bool childrenHeightMapLoaded = false;


	public QuadtreeLODNode( Mesh mesh, Transform transform, Material material )
	{		
		/*
		float[,] M = { {1, 2, 3}, {4, 5, 6}, {7, 8, 9} };
		float [,] M1 = GetSubHeightsMatrix (M, SubmatrixPosition.TOP_LEFT);
		Debug.LogFormat ("TOP_LEFT:\n {0}, {1}\n{2}, {3}", M1 [0,0], M1 [0,1], M1 [1,0], M1 [1,1]);
		float [,] M2 = GetSubHeightsMatrix (M, SubmatrixPosition.BOTTOM_LEFT);
		Debug.LogFormat ("TOP_RIGHT:\n {0}, {1}\n{2}, {3}", M2 [0,0], M2 [0,1], M2 [1,0], M2 [1,1]);
		float [,] M3 = GetSubHeightsMatrix (M, SubmatrixPosition.TOP_RIGHT);
		Debug.LogFormat ("BOTTOM_LEFT:\n {0}, {1}\n{2}, {3}", M3 [0,0], M3 [0,1], M3 [1,0], M3 [1,1]);
		float [,] M4 = GetSubHeightsMatrix (M, SubmatrixPosition.BOTTOM_RIGHT);
		Debug.LogFormat ("BOTTOM_RIGHT:\n {0}, {1}\n{2}, {3}", M4 [0,0], M4 [0,1], M4 [1,0], M4 [1,1]);
		*/

		// Copy given mesh.
		mesh_ = new Mesh ();
		mesh_.vertices = mesh.vertices;
		mesh_.triangles = mesh.triangles;
		mesh_.uv = mesh.uv;
		FlipUV ();
		mesh_.RecalculateNormals ();
		mesh_.RecalculateBounds ();

		// Make this mesh transform relative to parent.
		dumbGameObject_ = new GameObject ();
		transform_ = dumbGameObject_.transform;
		transform_.parent = transform;

		// Copy given material.
		material_ = new Material (Shader.Find ("Standard"));

		visible_ = true;

		children_ = new QuadtreeLODNode[]{ null, null, null, null };

		bottomLeftCoordinates_ = new Vector2 ( 416000,3067000 );
		topRightCoordinates_ = new Vector2 ( 466000,3117000 );

		LoadMap ();
		heightMapRequest = RequestHeightMap ( bottomLeftCoordinates_, topRightCoordinates_, 11 );
	}


	public QuadtreeLODNode( QuadtreeLODNode parent, Vector3 localPosition, Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates )
	{
		// Copy given mesh.
		mesh_ = new Mesh ();
		mesh_.vertices = parent.mesh_.vertices;
		mesh_.triangles = parent.mesh_.triangles;
		mesh_.uv = parent.mesh_.uv;
		mesh_.RecalculateNormals ();
		mesh_.RecalculateBounds ();

		// Make this mesh transform relative to parent.
		dumbGameObject_ = new GameObject ();
		transform_ = dumbGameObject_.transform;
		transform_.parent = parent.transform_;

		transform_.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
		transform_.localPosition = localPosition;
		
		material_ = new Material (Shader.Find ("Standard"));

		depth_ = parent.depth_ + 1;

		visible_ = true;

		bottomLeftCoordinates_ = bottomLeftCoordinates;
		topRightCoordinates_ = topRightCoordinates;
		
		LoadMap ();

		children_ = new QuadtreeLODNode[]{ null, null, null, null };

		heightMapRequest = RequestHeightMap ( bottomLeftCoordinates_, topRightCoordinates_, 11 );
	}


	private void FlipUV()
	{
		Vector2[] uv = mesh_.uv;
		for (int i=0; i<uv.Length; i++) {
			uv[i].x = 1.0f - uv[i].x;
			uv[i].y = 1.0f - uv[i].y;
		}
		mesh_.uv = uv;
	}


	public void SetVisible( bool visible )
	{
		visible_ = visible;
		if (visible_ == false && children_[0] != null) {
			for( int i = 0; i < children_.Length; i++ ){
				children_[i].SetVisible (false);
			} 
		}
	}


	public void Update()
	{
		float distanceToCamera = Vector3.Distance ( Camera.main.transform.position, transform_.position );
		Vector3 meshSize = Vector3.Scale (mesh_.bounds.size, transform_.lossyScale);

		// Subdivide the plane if camera is closer than a threshold.
		if( visible_ && distanceToCamera < THRESHOLD_FACTOR * meshSize.x ){
			// Create children if they don't exist.
			if( depth_ < MAX_DEPTH && children_[0] == null ){
				CreateChildren ( meshSize );
			}
			
			// Make this node invisible and children visible.
			if( AreChildrenLoaded() ){
				SetVisible (false);
				for( int i = 0; i < children_.Length; i++ ){
					children_[i].SetVisible (true);
				}
			}
		}else if( AreChildrenLoaded () && distanceToCamera >= THRESHOLD_FACTOR * meshSize.x ){
			SetVisible (true);
			for( int i = 0; i < children_.Length; i++ ){
				children_[i].SetVisible (false);
			}
		}

		// Update children.
		if (children_ [0] != null) {
			for (int i=0; i<4; i++) {
				children_ [i].Update ();
			}
		}

		if ( !textureLoaded && wwwService_.isDone) {
			textureLoaded = true;
			material_.mainTexture = wwwService_.texture;
			material_.mainTexture.wrapMode = TextureWrapMode.Clamp;
		}

		if (depth_ == 0 && !heightMapLoaded && heightMapRequest.isDone) {
			heightMapLoaded = true;
			SetHeightsMap( GetHeightsMatrix( heightMapRequest.text ) );
		}

		if (!childrenHeightMapLoaded && childrenHeightMapRequest != null && childrenHeightMapRequest.isDone) {
			childrenHeightMapLoaded = true;
			float [,] M = GetHeightsMatrix( childrenHeightMapRequest.text );

			Debug.Log ( "Setting children height maps" );

			children_ [0].SetHeightsMap( GetSubHeightsMatrix( M, SubmatrixPosition.TOP_LEFT ) );
			children_ [1].SetHeightsMap( GetSubHeightsMatrix( M, SubmatrixPosition.BOTTOM_LEFT ) );
			children_ [2].SetHeightsMap( GetSubHeightsMatrix( M, SubmatrixPosition.TOP_RIGHT ) );
			children_ [3].SetHeightsMap( GetSubHeightsMatrix( M, SubmatrixPosition.BOTTOM_RIGHT ) );
		}
	}


	private void CreateChildren( Vector3 meshSize )
	{
		Vector3 S = new Vector3(
			1.0f / transform_.lossyScale.x,
			1.0f / transform_.lossyScale.y,
			1.0f / transform_.lossyScale.z
			);
		Vector3[] childLocalPosition = new Vector3[]
		{
			Vector3.Scale ( new Vector3( -meshSize.x/4,0,meshSize.z/4 ), S ),
			Vector3.Scale ( new Vector3( -meshSize.x/4,0,-meshSize.z/4 ), S ),
			Vector3.Scale ( new Vector3( meshSize.x/4,0,meshSize.z/4), S ),
			Vector3.Scale ( new Vector3( meshSize.x/4,0,-meshSize.z/4), S )
		};
		
		float x0 = bottomLeftCoordinates_.x;
		float y0 = bottomLeftCoordinates_.y;
		float x1 = topRightCoordinates_.x;
		float y1 = topRightCoordinates_.y;
		
		float cx = (x0 + x1)/2.0f;
		float cy = (y0 + y1)/2.0f;
		
		Vector2[] childrenBottomLeftCoordinates = new Vector2[]
		{
			new Vector2( x0, cy ),
			new Vector2( x0, y0 ),
			new Vector2( cx, cy ),
			new Vector2( cx, y0 )
		};
		
		Vector2[] childrenTopLeftCoordinates = new Vector2[]
		{
			new Vector2( cx, y1 ),
			new Vector2( cx, cy ),
			new Vector2( x1, y1 ),
			new Vector2( x1, cy )
		};
		
		for( int i=0; i<4; i++ ){
			children_[i] = new QuadtreeLODNode( this, childLocalPosition[i], childrenBottomLeftCoordinates[i], childrenTopLeftCoordinates[i] ); 
		}

		int CHILDREN_RESOLUTION = 11 * 2 - 1;
		Debug.Log ("Requesting children height map - " + CHILDREN_RESOLUTION );
		childrenHeightMapRequest = RequestHeightMap (bottomLeftCoordinates_, topRightCoordinates_, CHILDREN_RESOLUTION );
	}


	public void Render()
	{
		if (visible_) {
			Graphics.DrawMesh (mesh_, transform_.localToWorldMatrix, material_, 0);
		}
		if (AreChildrenLoaded()) {
			for (int i=0; i<4; i++) {
				children_ [i].Render ();
			}
		}
	}


	private void LoadMap() {
		string fixedUrl = "http://idecan1.grafcan.com/ServicioWMS/OrtoExpress?SERVICE=WMS&LAYERS=ortoexpress&REQUEST=GetMap&VERSION=1.1.0&FORMAT=image/jpeg&SRS=EPSG:32628&WIDTH=128&HEIGHT=128&REFERER=CAPAWARE";
		string bboxUrlQuery = 
			"&BBOX=" + bottomLeftCoordinates_.x + "," +
				bottomLeftCoordinates_.y + "," +
			topRightCoordinates_.x + "," +
				topRightCoordinates_.y;
		string url = fixedUrl + bboxUrlQuery;
		wwwService_ = new WWW(url);
	}


	private static WWW RequestHeightMap( 
	                                    Vector2 bottomLeftCoordinates, 
	                                    Vector2 topRightCoordinates,
	                                    int N ){
		Vector2 wcsResolution = new Vector2 (
			(topRightCoordinates.x - bottomLeftCoordinates.x) / N,
			(topRightCoordinates.y - bottomLeftCoordinates.y) / N
		);

		string fixedUrl = "http://www.idee.es/wcs/IDEE-WCS-UTM28N/wcsServlet?REQUEST=GetCoverage&SERVICE=WCS&VERSION=1.0.0&FORMAT=AsciiGrid&COVERAGE=MDT_canarias&CRS=EPSG:25828&REFERER=CAPAWARE";
		string bboxUrlQuery = 
			"&BBOX=" + bottomLeftCoordinates.x + "," +
			bottomLeftCoordinates.y + "," +
			topRightCoordinates.x + "," +
			topRightCoordinates.y;
		string resolutionUrlQuery =
			"&RESX=" + wcsResolution.x +
			"&RESY=" + wcsResolution.y;

		string url = fixedUrl + bboxUrlQuery + resolutionUrlQuery;

		Debug.Log ("heightMap URL - " + url);

		return new WWW( url );
	}


	private bool AreChildrenLoaded(){
		if (children_ [0] != null && childrenHeightMapLoaded) {
			for (int i = 0; i < 4; i++) {
				if (children_ [i].textureLoaded == false || !children_[i].heightMapRequest.isDone ) {
					return false;
				}
			}
			return true;
		} else {
			return false;
		}
	}


	private float[,] GetHeightsMatrix( string heightMapSpec ){
		string[] specLines = heightMapSpec.Split ('\n');
		const int HEIGHTS_START_LINE = 6;
		int N_COLUMNS = int.Parse ( specLines [0].Split (new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries) [1] );
		int N_ROWS = int.Parse ( specLines [1].Split (new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries) [1] );

		float[,] heightsMatrix = new float[N_ROWS,N_COLUMNS];
		
		for (int i=0; i<N_ROWS; i++) {
			string[] heightsStrLine = specLines[HEIGHTS_START_LINE+i].Split (' ');
			
			for(int j=0; j<N_COLUMNS; j++){
				heightsMatrix[i,j] = float.Parse ( heightsStrLine[j] );
			}
		}
		
		return heightsMatrix;
	}

	enum SubmatrixPosition {TOP_LEFT, TOP_RIGHT, BOTTOM_LEFT, BOTTOM_RIGHT};

	private float[,] GetSubHeightsMatrix ( float[,] heightsMatrix, 
	                                       SubmatrixPosition submatrixPosition )
	{
		int N = heightsMatrix.GetLength (0);

		switch (submatrixPosition) {
			case SubmatrixPosition.TOP_LEFT:
				return GetSubMatrix( heightsMatrix, 0, 0, N/2+1, N/2+1 );
			case SubmatrixPosition.TOP_RIGHT:
				return GetSubMatrix( heightsMatrix, 0, N/2, N/2+1, N );
			case SubmatrixPosition.BOTTOM_LEFT:
				return GetSubMatrix( heightsMatrix, N/2, 0, N, N/2+1 );
			case SubmatrixPosition.BOTTOM_RIGHT:
				return GetSubMatrix( heightsMatrix, N/2, N/2, N, N );
			default:
				return GetSubMatrix( heightsMatrix, 0, 0, N, N );
		}
	}


	private float[,] GetSubMatrix( float[,] M, 
	                                int startRow, 
	                                int startColumn,
	                                int lastRow,
	                                int lastColumn )
	{
		int N_ROWS = lastRow - startRow;
		int N_COLUMNS = lastColumn - startColumn;

		float[,] subMatrix = new float[N_ROWS,N_COLUMNS];

		for (int i=0; i<N_ROWS; i++) {
			for(int j=0; j<N_COLUMNS; j++){
				subMatrix[i,j] = M[i+startRow,j+startColumn];
			}
		}
		return subMatrix;
	}


	private void SetHeightsMap( float[,] heights )
	{
		Vector3[] vertices = mesh_.vertices;

		if (depth_ > 0) { Debug.Log ( "Starting child" ); }

		int N_ROWS = heights.GetLength(0);
		for (int row=0; row<N_ROWS; row++) {
			// FIXME: This is forcing N_COLUMS = N_ROWS.
			int N_COLUMNS = N_ROWS;
			for (int column=0; column<N_COLUMNS; column++) {
				int VERTEX_INDEX = row * N_COLUMNS + column;
				vertices[VERTEX_INDEX].y = heights[row,N_COLUMNS-1-column] / 1000.0f; /// maxHeight;
				if (depth_ > 0 && row == 0) {
					Debug.Log ( "Vertex[" + 
					           VERTEX_INDEX + 
					           "]: " + 
					           vertices[VERTEX_INDEX] +
					           " -> height value[" +
					           row +
					           ", " +
					           (N_COLUMNS-1-column) + 
					           "](" +
					           heights[row,N_COLUMNS-1-column] +
					           ")");
				}
			}
		}
		
		mesh_.vertices = vertices;
		mesh_.RecalculateBounds ();
		mesh_.RecalculateNormals ();
	}
}
