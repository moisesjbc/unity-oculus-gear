using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapTexturesManager : OnlineResourcesManager
{
	public string RequestTexture( 
	                            Vector2 bottomLeftCoordinates, 
	                            Vector2 topRightCoordinates
	                             ){
		string newId = GenerateID (bottomLeftCoordinates, topRightCoordinates );
		
		if ( ResourceNotRequested( newId ) ) {
			string fixedUrl = "http://idecan1.grafcan.com/ServicioWMS/OrtoExpress?SERVICE=WMS&LAYERS=ortoexpress&REQUEST=GetMap&VERSION=1.1.0&FORMAT=image/jpeg&SRS=EPSG:32628&WIDTH=128&HEIGHT=128&REFERER=CAPAWARE";
			string bboxUrlQuery = 
				"&BBOX=" + bottomLeftCoordinates.x + "," +
					bottomLeftCoordinates.y + "," +
					topRightCoordinates.x + "," +
					topRightCoordinates.y;
			string url = fixedUrl + bboxUrlQuery;
		
			requests_ [newId] = new WWW (url);

			Debug.LogFormat ("Requesting texture with id [{0}] - NOT cached", newId);
			Debug.Log ("Texture URL - " + url);
		} else {
			Debug.LogFormat ("Requesting texture with id [{0}] - Cached!", newId);
		}

		return newId;
	}


	protected string GenerateID( Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates )
	{
		return 
			"texture-" + 
			bottomLeftCoordinates.x + "-" + 
			bottomLeftCoordinates.y + "-" +
			topRightCoordinates.x + "-" + 
			topRightCoordinates.y +
			".jpg";
	}


	public Texture2D GetTexture( string id )
	{
		byte[] jpgTextureData = null;

		// Check if we have a finished request with the same ID
		// and save the result to a file.
		if ( requests_.ContainsKey(id) && requests_ [id].isDone ){
			jpgTextureData = requests_[id].texture.EncodeToJPG();
			File.WriteAllBytes( FilePath( id ), jpgTextureData );
		}
		
		// If the request hasn't finished, the file won't exist
		// yet.
		if( !File.Exists ( FilePath( id ) ) ){
			return null;
		}

		// The result file for the requested ID, exists. Parse
		// its contents and return the height matrix.
		if (jpgTextureData == null) {
			jpgTextureData = File.ReadAllBytes ( FilePath ( id ) );
		}
		Texture2D texture = new Texture2D (2, 2);
		texture.LoadImage (jpgTextureData);
		return texture;
	}
}
