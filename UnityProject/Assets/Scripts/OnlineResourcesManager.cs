using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public abstract class OnlineResourcesManager {
	protected Dictionary<string,WWW> requests_ = new Dictionary<string,WWW>();

	protected bool ResourceNotRequested( string id )
	{
		if( !File.Exists (FilePath (id) ) ){
			Debug.LogFormat ( "File {0} doesn't exist", FilePath(id) );
		}
		if( !requests_.ContainsKey (id) ){
			Debug.LogFormat ( "Request {0} doesn't exist", id );
		}
		return !File.Exists (FilePath (id)) && !requests_.ContainsKey (id);
	}

	protected string FilePath( string id )
	{
		return Application.persistentDataPath + "/" + id;
	}
}
