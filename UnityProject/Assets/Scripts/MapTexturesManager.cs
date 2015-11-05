using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapTexturesManager : OnlineResourcesManager
{
	public delegate void MapTextureCallback( Texture2D texture );


	public void RequestTexture( 
	                            Vector2 bottomLeftCoordinates, 
	                            Vector2 topRightCoordinates,
	                            MapTextureCallback callback
	                             ){
		string fixedUrl = "http://idecan1.grafcan.com/ServicioWMS/OrtoExpress?SERVICE=WMS&LAYERS=ortoexpress&REQUEST=GetMap&VERSION=1.1.0&FORMAT=image/jpeg&SRS=EPSG:32628&WIDTH=128&HEIGHT=128&REFERER=CAPAWARE";
		string bboxUrlQuery = 
			"&BBOX=" + bottomLeftCoordinates.x + "," +
				bottomLeftCoordinates.y + "," +
				topRightCoordinates.x + "," +
				topRightCoordinates.y;
		string url = fixedUrl + bboxUrlQuery;
	
		Debug.LogFormat ("Requesting texture: {0}", url);

		StartCoroutine (RequestURL (url, callback));
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


	private IEnumerator RequestURL( string url, MapTextureCallback callback )
	{
		WWW www = new WWW (url);
		
		yield return www;
		
		callback ( www.texture );
	}
}
