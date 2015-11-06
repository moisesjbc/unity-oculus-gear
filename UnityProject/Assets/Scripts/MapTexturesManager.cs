using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class MapTexturesManager : OnlineResourcesManager<Texture2D>
{
	protected override string GenerateID( Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates, int resolution )
	{
		return 
			"texture-" + 
				bottomLeftCoordinates.x + "-" + 
				bottomLeftCoordinates.y + "-" +
				topRightCoordinates.x + "-" + 
				topRightCoordinates.y + "-res" +
				resolution +
				".jpg";
	}


	protected override string GenerateURL( Vector2 bottomLeftCoordinates, Vector2 topRightCoordinates, int resolution )
	{
		string fixedUrl = "http://idecan1.grafcan.com/ServicioWMS/OrtoExpress?SERVICE=WMS&LAYERS=ortoexpress&REQUEST=GetMap&VERSION=1.1.0&FORMAT=image/jpeg&SRS=EPSG:32628&WIDTH=128&HEIGHT=128&REFERER=CAPAWARE";
		string bboxUrlQuery = 
			"&BBOX=" + bottomLeftCoordinates.x + "," +
			bottomLeftCoordinates.y + "," +
			topRightCoordinates.x + "," +
			topRightCoordinates.y;
		return fixedUrl + bboxUrlQuery;
	}


	protected override Texture2D RetrieveResourceFromWWW( WWW www )
	{
		return www.texture;
	}

	
	protected override Texture2D RetrieveResourceFromFile( string resourceID )
	{
		byte[] textureRawData = File.ReadAllBytes (FilePath (resourceID));

		Texture2D texture = new Texture2D (2, 2);
		texture.LoadImage ( textureRawData );
		return texture;
	}


	protected override void WriteWWWToFile( string resourceID, WWW www )
	{
		File.WriteAllBytes (FilePath (resourceID), www.texture.EncodeToJPG ());
	}
}
