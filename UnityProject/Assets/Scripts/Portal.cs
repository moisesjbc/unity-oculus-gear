using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.GetComponent<CameraMovement>() != null ){
			GameObject.Find ("Level").GetComponent<LevelManagement>().Reset(LevelManagement.Island.TENERIFE);
		}
	}
}
