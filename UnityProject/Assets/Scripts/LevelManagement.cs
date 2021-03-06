﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class LevelManagement : MonoBehaviour {
	
	public enum Island {
		GRAN_CANARIA,
		TENERIFE,
		LA_PALMA
	};
	
	public Island island;
	private List<Vector3> pickupPositions = new List<Vector3>();
	private Text scoreText;
	private PickupCollection pickupsCollector;

	// Use this for initialization
	void Awake () {
		Vector2 bottomLeftCoordinates = Vector2.zero;
		Vector2 topRightCoordinates = Vector2.zero;

		switch (island) {
			case Island.GRAN_CANARIA:
				bottomLeftCoordinates = new Vector2 ( 416000,3067000 );
				topRightCoordinates = new Vector2 ( 466000,3117000 );
				
				pickupPositions.Add( new Vector3( 0f, 3f, -15f ) );
				pickupPositions.Add( new Vector3( -3.6f, 5.2f, 5f ) );
			break;
			case Island.TENERIFE:
				bottomLeftCoordinates = new Vector2 ( 310000,3090000 );
				topRightCoordinates = new Vector2 ( 392000,3172000 );
			break;
			case Island.LA_PALMA:
				bottomLeftCoordinates = new Vector2 ( 192500,3145000 );
				topRightCoordinates = new Vector2 ( 247500,3200000 );
			break;
		}

		foreach( Vector3 pickupPosition in pickupPositions )
		{
			Instantiate( Resources.Load("Pickup"), pickupPosition, Quaternion.identity );
		}

		QuadtreeLODPlane[] maps = gameObject.GetComponentsInChildren<QuadtreeLODPlane> ();
	
		scoreText = GameObject.Find ("ScoreText").GetComponent<UnityEngine.UI.Text> ();
		pickupsCollector = gameObject.GetComponentInChildren<PickupCollection> ();

		foreach( QuadtreeLODPlane map in maps ){
			map.Reset( bottomLeftCoordinates, topRightCoordinates );
		}


	}
	
	// Update is called once per frame
	void Update () {
		scoreText.text = "Score: " + pickupsCollector.score + " / " + pickupPositions.Count.ToString();
	}
}
