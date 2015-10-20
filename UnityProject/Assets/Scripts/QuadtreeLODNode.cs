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

	
	public QuadtreeLODNode( Mesh mesh, Transform transform, Material material )
	{		
		// Copy given mesh.
		mesh_ = new Mesh ();
		mesh_.vertices = mesh.vertices;
		mesh_.triangles = mesh.triangles;
		mesh_.uv = mesh.uv;
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
		heightMapRequest = RequestHeightMap ();
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

		heightMapRequest = RequestHeightMap ();
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
			Vector3.Scale ( new Vector3( meshSize.x/4,0,-meshSize.z/4 ), S ),
			Vector3.Scale ( new Vector3( meshSize.x/4,0,meshSize.z/4 ), S ),
			Vector3.Scale ( new Vector3( -meshSize.x/4,0,-meshSize.z/4), S ),
			Vector3.Scale ( new Vector3( -meshSize.x/4,0,meshSize.z/4), S )
		};
		
		float x0 = bottomLeftCoordinates_.x;
		float y0 = bottomLeftCoordinates_.y;
		float x1 = topRightCoordinates_.x;
		float y1 = topRightCoordinates_.y;
		
		float cx = (x0 + x1)/2.0f;
		float cy = (y0 + y1)/2.0f;
		
		Vector2[] childrenTopLeftCoordinates = new Vector2[]
		{
			new Vector2( x0, cy ),
			new Vector2( x0, y0 ),
			new Vector2( cx, cy ),
			new Vector2( cx, y0 )
		};
		
		Vector2[] childrenBottomLeftCoordinates = new Vector2[]
		{
			new Vector2( cx, y1 ),
			new Vector2( cx, cy ),
			new Vector2( x1, y1 ),
			new Vector2( x1, cy )
		};
		
		for( int i=0; i<4; i++ ){
			children_[i] = new QuadtreeLODNode( this, childLocalPosition[i], childrenTopLeftCoordinates[i], childrenBottomLeftCoordinates[i] ); 
		}
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


	private WWW RequestHeightMap(){
		// FIXME: Requesting a real RESY seems to not work when RESX != RESY.
		Vector2 wcsResolution = new Vector2 (
			(bottomLeftCoordinates_.x - topRightCoordinates_.x) / 10,
			(bottomLeftCoordinates_.x - topRightCoordinates_.x) / 10
		);

		string fixedUrl = "http://www.idee.es/wcs/IDEE-WCS-UTM28N/wcsServlet?REQUEST=GetCoverage&SERVICE=WCS&VERSION=1.0.0&FORMAT=AsciiGrid&COVERAGE=MDT_canarias&CRS=EPSG:25828&REFERER=CAPAWARE";
		string bboxUrlQuery = 
			"&BBOX=" + bottomLeftCoordinates_.x + "," +
				bottomLeftCoordinates_.y + "," +
				topRightCoordinates_.x + "," +
				topRightCoordinates_.y;
		string resolutionUrlQuery =
			"&RESX=" + wcsResolution.x +
			"&RESY=" + wcsResolution.y;

		string url = fixedUrl + bboxUrlQuery + resolutionUrlQuery;

		return new WWW( url );
	}


	private bool AreChildrenLoaded(){
		if (children_ [0] != null) {
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
}
