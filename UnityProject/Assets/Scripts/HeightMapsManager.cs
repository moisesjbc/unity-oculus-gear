using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class HeightMapsManager : OnlineResourcesManager
{
	public delegate void HeightMapCallback( float [,] heights );

	public void RequestHeightMap( 
	                            Vector2 bottomLeftCoordinates, 
	                            Vector2 topRightCoordinates,
	                            int N,
	                            HeightMapCallback callback ){
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

		StartCoroutine (RequestURL (url, callback));
	}


	private IEnumerator RequestURL( string url, HeightMapCallback callback )
	{
		WWW www = new WWW (url);
		
		yield return www;
		
		callback ( ParseHeightMatrix ( www.text ) );
	}


	protected string GenerateID( Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates, int N )
	{
		return 
			"heightmap-" + 
			bottomLeftCoordinates.x + "-" + 
			bottomLeftCoordinates.y + "-" +
			topRightCoordinates.x + "-" + 
			topRightCoordinates.y + "-" +
			N;
	}


	private float[,] ParseHeightMatrix( string heightMapSpec ){
		string[] specLines = heightMapSpec.Split ('\n');
		const int HEIGHTS_START_LINE = 6;
		int N_COLUMNS = int.Parse ( specLines [0].Split (new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries) [1] );
		int N_ROWS = int.Parse ( specLines [1].Split (new string[]{" "}, System.StringSplitOptions.RemoveEmptyEntries) [1] );
		
		float[,] heightsMatrix = new float[N_ROWS,N_COLUMNS];
		
		for (int i=0; i<N_ROWS; i++) {
			string[] heightsStrLine = specLines[HEIGHTS_START_LINE+i].Split (' ');
			
			for(int j=0; j<N_COLUMNS; j++){
				heightsMatrix[i,j] = float.Parse ( heightsStrLine[j] );
				heightsMatrix[i,j] = Mathf.Max( heightsMatrix[i,j], 0.0f );
			}
		}
		
		return heightsMatrix;
	}
}
