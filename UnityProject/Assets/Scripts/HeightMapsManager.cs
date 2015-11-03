using UnityEngine;
using System.Collections;

public class HeightMapsManager 
{
	public WWW RequestHeightMap( 
	                            Vector2 bottomLeftCoordinates, 
	                            Vector2 topRightCoordinates,
	                            int N ){
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
		
		return new WWW( url );
	}
}
