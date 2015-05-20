using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorBoard : MonoBehaviour 
{
	const int MAX_COLOR_GROUP_COUNT = 100;
	const int MAX_COLOR_COUNT = 20;
	const string COLOR_BOARD_KEY = "colorboard";
	static List<List<Color>> mData = new List<List<Color>>();
	public static void LoadColors()
	{
		string colorKey = "";
		List<Color> colors = new List<Color>();
		for (int iG = 0; iG < MAX_COLOR_GROUP_COUNT; iG++) 
		{
			for (int iC = 0; iC < MAX_COLOR_COUNT; iC++) 
			{
				colorKey = COLOR_BOARD_KEY + "." + iG + "." + iC;
				Color cl = Property.GetColor(colorKey);

			}

		}
	}
}
