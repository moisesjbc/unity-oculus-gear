using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	private LevelManagement.Island dstIsland_;

	public void Awake()
	{
		SetDstIsland ( LevelManagement.Island.GRAN_CANARIA );
	}


	public void SetDstIsland( LevelManagement.Island dstIsland )
	{
		dstIsland_ = dstIsland;

		switch (dstIsland_) {
			case LevelManagement.Island.GRAN_CANARIA:
				GetComponentInChildren<TextMesh>().text = "Gran Canaria";
				break;
			case LevelManagement.Island.TENERIFE:
				GetComponentInChildren<TextMesh>().text = "Tenerife";
				break;
			case LevelManagement.Island.LA_PALMA:
				GetComponentInChildren<TextMesh>().text = "La Palma";
				break;
		}
	}


	public void OnTriggerEnter( Collider other )
	{
		if( other.gameObject.GetComponent<CameraMovement>() != null ){
			GameObject.Find ("Level").GetComponent<LevelManagement>().Reset(dstIsland_);
		}
	}
}
