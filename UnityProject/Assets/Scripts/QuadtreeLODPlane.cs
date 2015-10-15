using UnityEngine;
using System.Collections;

public class QuadtreeLODPlane : MonoBehaviour {
	private GameObject[] child_;

	void Awake () {
		child_ = new GameObject[4]{ null, null, null, null };
	}


	void Update () {
		float distanceToCamera = Vector3.Distance ( Camera.main.transform.position, transform.position );
		//Debug.Log ("distanceToCamera: " + distanceToCamera);
		Vector3 parenSize = GetComponent<Renderer>().bounds.size;

		// Subdivide the plane if camera is closer than a threshold.
		if( distanceToCamera < 2.0f * parenSize.x ){
			// Create children if they don't exist.
			if( child_[0] == null ){
				Vector3 S = new Vector3(
					1.0f / transform.lossyScale.x,
					1.0f / transform.lossyScale.y,
					1.0f / transform.lossyScale.z
					);
				Vector3[] childLocalPosition = new Vector3[]
				{
					Vector3.Scale ( new Vector3( parenSize.x/4,0,-parenSize.z/4 ), S ),
					Vector3.Scale ( new Vector3( parenSize.x/4,0,parenSize.z/4 ), S ),
					Vector3.Scale ( new Vector3( -parenSize.x/4,0,-parenSize.z/4), S ),
					Vector3.Scale ( new Vector3( -parenSize.x/4,0,parenSize.z/4), S )
				};
				
				Color32[] childColors = new Color32[]
				{
					new Color32(255, 0, 0, 255),
					new Color32(0, 255, 0, 255),
					new Color32(0, 0, 255, 255),
					new Color32(255, 0, 255, 255)
				};
				
				for( int i=0; i<4; i++ ){
					// FIXME: This clones the current instance, while a 
					// better way would be to simply instance the prefab, 
					// but Unity doesn't support native prefabs natively.
					child_[i] = Instantiate ( this.gameObject );
				}
				
				for( int i=0; i<4; i++ ){
					child_[i].GetComponent<Renderer> ().material.mainTexture = Texture2D.whiteTexture;
					child_[i].GetComponent<Renderer> ().material.color = childColors[i];
					child_[i].transform.parent = transform;
					child_[i].transform.localScale = new Vector3( 0.5f, 0.5f, 0.5f );
					child_[i].transform.localPosition = childLocalPosition[i];
				}
			}

			// Make this node invisible and children visible.
			GetComponent<MeshRenderer>().enabled = false;
			for( int i = 0; i < child_.Length; i++ ){
				child_[i].GetComponent<MeshRenderer>().enabled = true;
			}
		}else if( child_[0] != null && distanceToCamera >= 2.0f * parenSize.x ){
			// Make this node visible and children invisible.
			GetComponent<MeshRenderer>().enabled = true;
			for( int i = 0; i < child_.Length; i++ ){
				child_[i].GetComponent<MeshRenderer>().enabled = false;
			}
		}
	}
}
