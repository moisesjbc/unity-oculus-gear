//#define CACHE_RESOURCES
// Uncomment previous line to activate resources caching.

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public abstract class OnlineResourcesManager<Resource> : MonoBehaviour {
#if CACHE_RESOURCES
	protected Dictionary<string,WWW> requests_ = new Dictionary<string,WWW>();
#endif

	public delegate void ResourceCallback( Resource resource );

	public void RequestResource( Vector2 bottomLeftCoordinates, 
	                            Vector2 topRightCoordinates,
	                            int resolution,
	                            ResourceCallback callback ){
		string newId = GenerateID (bottomLeftCoordinates, topRightCoordinates, resolution );

#if CACHE_RESOURCES
		if( ResourceAlreadyRequested( newId ) ){
			Debug.LogFormat ("Requesting texture: {0} - CACHED", newId);
			if( File.Exists ( FilePath ( newId ) ) ){
				callback( RetrieveResourceFromFile( newId ) );
			}else{
				StartCoroutine( WaitForRequestedResource( newId, callback ) );
			}
			return;
		}
#endif
		string url = GenerateURL ( bottomLeftCoordinates, topRightCoordinates, resolution );
		
		Debug.LogFormat ("Requesting resource: {0}", url);
		
		StartCoroutine (RequestURL (newId, url, callback));
	}

#if CACHE_RESOURCES
	protected bool ResourceAlreadyRequested( string id )
	{
		return File.Exists (FilePath (id)) || requests_.ContainsKey (id);
	}
#endif

	protected string FilePath( string id )
	{
		return Application.persistentDataPath + "/" + id;
	}


	protected abstract string GenerateID( Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates, int resolution );
	protected abstract string GenerateURL( Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates, int resolution );


	protected IEnumerator RequestURL( string id, string url, ResourceCallback callback )
	{
		#if CACHE_RESOURCES
		requests_[id] = new WWW (url);
		
		yield return requests_[id];

		WriteWWWToFile( id, requests_[id] );
		
		callback( RetrieveResourceFromFile( id ) );
		#else
		WWW www = new WWW (url);
		
		yield return www;
		
		callback ( RetrieveResourceFromWWW ( www ) );
		#endif
	}


	protected abstract Resource RetrieveResourceFromWWW( WWW www );
	protected abstract Resource RetrieveResourceFromFile( string resourceID );
	protected abstract void WriteWWWToFile( string resourceID, WWW www );


#if CACHE_RESOURCES
	IEnumerator WaitForRequestedResource( string resourceID, ResourceCallback callback )
	{
		yield return requests_[resourceID];
		
		WriteWWWToFile( resourceID, requests_ [resourceID] );
		
		callback( RetrieveResourceFromFile( resourceID ) );
	}
#endif
}
