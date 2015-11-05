using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public abstract class OnlineResourcesManager : MonoBehaviour {
	protected Dictionary<string,WWW> requests_ = new Dictionary<string,WWW>();

	protected bool ResourceAlreadyRequested( string id )
	{
		return File.Exists (FilePath (id)) || requests_.ContainsKey (id);
	}

	protected string FilePath( string id )
	{
		return Application.persistentDataPath + "/" + id;
	}
}
