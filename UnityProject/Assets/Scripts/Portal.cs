using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public LevelManagement.Island dstIsland;

	public void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.GetComponent<CameraMovement>() != null ){
			GameObject.Find ("Level").GetComponent<LevelManagement>().Reset(dstIsland);
		}
	}
}
