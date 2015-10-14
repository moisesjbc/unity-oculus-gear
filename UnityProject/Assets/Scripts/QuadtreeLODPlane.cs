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
		if( child_[0] == null && distanceToCamera < 2.0f * parenSize.x ){
			Debug.Log ("Distance to camera: " + distanceToCamera);
			Debug.Log ("parenSize.x: " + parenSize.x);
			Vector3[] childLocalPosition = new Vector3[]
			{
				new Vector3( parenSize.x/4,0,-parenSize.z/4 ),
				new Vector3( parenSize.x/4,0,parenSize.z/4 ),
				new Vector3( -parenSize.x/4,0,-parenSize.z/4 ),
				new Vector3( -parenSize.x/4,0,parenSize.z/4 )
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
				child_[i].transform.localScale = new Vector3( 0.5f, 1.0f, 0.5f );
				child_[i].transform.localPosition = childLocalPosition[i];
			}

			Debug.Log ( GetComponent<Renderer>().bounds.size );
			GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
