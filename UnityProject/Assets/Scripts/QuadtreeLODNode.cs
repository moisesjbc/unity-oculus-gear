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
	
	QuadtreeLODNode[] children_;
	bool leafNode = true;
	
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
		material_.color = Color.red;

		visible_ = true;

		children_ = new QuadtreeLODNode[]{ null, null, null, null };
	}


	public QuadtreeLODNode( QuadtreeLODNode parent, Color color, Vector3 localPosition )
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

		// Copy given material.
		material_ = new Material (Shader.Find ("Standard"));
		material_.color = color;

		visible_ = true;
		
		children_ = new QuadtreeLODNode[]{ null, null, null, null };
	}


	public void SetVisible( bool visible )
	{
		visible_ = visible;
	}


	public void Update()
	{
		float distanceToCamera = Vector3.Distance ( Camera.main.transform.position, transform_.position );
		Vector3 meshSize = Vector3.Scale (mesh_.bounds.size, transform_.lossyScale);

		// Subdivide the plane if camera is closer than a threshold.
		if( leafNode && distanceToCamera < 2.0f * meshSize.x ){
			leafNode = false;
			Debug.Log ("Creating children");
			// Create children if they don't exist.
			if( children_[0] == null ){
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
				
				Color32[] childColors = new Color32[]
				{
					new Color32(255, 0, 0, 255),
					new Color32(0, 255, 0, 255),
					new Color32(0, 0, 255, 255),
					new Color32(255, 0, 255, 255)
				};
								
				for( int i=0; i<4; i++ ){
					children_[i] = new QuadtreeLODNode( this, childColors[i], childLocalPosition[i] ); 
				}

				for( int i=0; i<4; i++ ){
					Debug.Log (children_[i].transform_.localScale);
				}
			}
			
			// Make this node invisible and children visible.
			SetVisible (false);
			for( int i = 0; i < children_.Length; i++ ){
				children_[i].SetVisible (true);
			}
		}else if( !leafNode && children_[0] != null && distanceToCamera >= 2.0f * meshSize.x ){
			leafNode = true;
			SetVisible (true);
			for( int i = 0; i < children_.Length; i++ ){
				children_[i].SetVisible (false);
			}
		}

		// Update children.
		if (!leafNode && children_ [0] != null) {
			for (int i=0; i<4; i++) {
				children_ [i].Update ();
			}
		}
	}


	public void Render()
	{
		if (visible_) {
			Graphics.DrawMesh (mesh_, transform_.localToWorldMatrix, material_, 0);
		}
		if (children_ [0] != null) {
			for (int i=0; i<4; i++) {
				children_ [i].Render ();
			}
		}
	}
}
