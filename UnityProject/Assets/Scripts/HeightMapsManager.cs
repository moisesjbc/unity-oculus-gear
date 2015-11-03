using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HeightMapsManager 
{
	Dictionary<string,WWW> requests_ = new Dictionary<string,WWW>();

	public string RequestHeightMap( 
	                            Vector2 bottomLeftCoordinates, 
	                            Vector2 topRightCoordinates,
	                            int N ){
		string newId = GenerateID (bottomLeftCoordinates, topRightCoordinates, N);

		if (!requests_.ContainsKey (newId)) {
			string fixedUrl = "http://www.idee.es/wcs/IDEE-WCS-UTM28N/wcsServlet?REQUEST=GetCoverage&SERVICE=WCS&VERSION=1.0.0&FORMAT=AsciiGrid&COVERAGE=MDT_canarias&CRS=EPSG:25828&REFERER=CAPAWARE";
			string bboxUrlQuery = 
			"&BBOX=" + bottomLeftCoordinates.x + "," +
				bottomLeftCoordinates.y + "," +
				topRightCoordinates.x + "," +
				topRightCoordinates.y;
			string dimensionsUrlQuery =
			"&WIDTH=" + N +
				"&HEIGHT=" + N;
		
			string url = fixedUrl + bboxUrlQuery + dimensionsUrlQuery;
		
			Debug.Log ("heightMap URL - " + url);

			requests_[newId] = new WWW(url);
		}
		
		return newId;
	}


	private string GenerateID( Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates, int N )
	{
		return 
			"heightmap-" + 
			bottomLeftCoordinates.x + "-" + 
			bottomLeftCoordinates.y + "-" +
			topRightCoordinates.x + "-" + 
			topRightCoordinates.y + "-" +
			N;
	}


	private string FilePath( string id )
	{
		return Application.persistentDataPath + "/" + id;
	}


	public WWW GetRequest(string id)
	{
		if ( requests_ [id].isDone ){
			StreamWriter file = new StreamWriter( FilePath( id ) );
			file.Write ( requests_ [id].text );
			file.Close();
		}

		return requests_[id];
	}
}
