//#define CACHE_RESOURCES
// Uncomment previous line to activate resources caching.

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
		string newId = GenerateID (bottomLeftCoordinates, topRightCoordinates);

#if CACHE_RESOURCES
		if( ResourceAlreadyRequested( newId ) ){
			Debug.LogFormat ("Requesting texture: {0} - CACHED", newId);
			if( File.Exists ( FilePath ( newId ) ) ){
				Texture2D texture = new Texture2D( 2, 2 );
				texture.LoadImage( File.ReadAllBytes( FilePath( newId ) ) );
				callback( texture );
			}else{
				StartCoroutine( WaitForRequestedTexture( newId, callback ) );
			}
			return;
		}
#endif
		string fixedUrl = "http://idecan1.grafcan.com/ServicioWMS/OrtoExpress?SERVICE=WMS&LAYERS=ortoexpress&REQUEST=GetMap&VERSION=1.1.0&FORMAT=image/jpeg&SRS=EPSG:32628&WIDTH=128&HEIGHT=128&REFERER=CAPAWARE";
		string bboxUrlQuery = 
			"&BBOX=" + bottomLeftCoordinates.x + "," +
				bottomLeftCoordinates.y + "," +
				topRightCoordinates.x + "," +
				topRightCoordinates.y;
		string url = fixedUrl + bboxUrlQuery;
		
		Debug.LogFormat ("Requesting texture: {0}", url);
		
		StartCoroutine (RequestURL (newId, url, callback));
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


	private IEnumerator RequestURL( string id, string url, MapTextureCallback callback )
	{
#if CACHE_RESOURCES
		requests_[id] = new WWW (url);

		yield return requests_[id];

		byte[] jpgTextureData = requests_[id].texture.EncodeToJPG();
		File.WriteAllBytes( FilePath( id ), jpgTextureData );

		callback( requests_[id].texture );
#else
		WWW www = new WWW (url);
		
		yield return www;

		callback ( www.texture );
#endif
	}


#if CACHE_RESOURCES
	private IEnumerator WaitForRequestedTexture( string id, MapTextureCallback callback )
	{
		yield return requests_[id];
		callback ( requests_[id].texture );
	}
#endif
}
