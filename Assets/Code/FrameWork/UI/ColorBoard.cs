using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorBoard : MonoBehaviour 
{
	const int MAX_COLOR_GROUP_COUNT = 100;
	const int MAX_COLOR_COUNT = 20;
	const string COLOR_BOARD_KEY = "colorboard";
	const string ColorCountKey = "colorcount";
	static List<Color[]> mData = new List<Color[]>();

	public static void Init(Property props)
	{
		LoadColors (props);
	}

	public static void LoadColors(Property prop)
	{
		string colorKey = "";
		for (int iG = 0; iG < MAX_COLOR_GROUP_COUNT; iG++) 
		{
			int colorCount = prop.GetInt(COLOR_BOARD_KEY + "." + iG + "." + ColorCountKey);
			Color[] colors = new Color[colorCount];
			for (int iC = 0; iC < colorCount; iC++) 
			{
				colorKey 	= COLOR_BOARD_KEY + "." + iG + "." + iC;
				Color cl 	= prop.GetColor(colorKey);
				colors[iC] 	= cl;
			}
			if (colorCount > 0) {
				mData.Add(colors);
			}
		}
	}

	public static Color[] GetColorArray(int arrayIndex)
	{
		if (arrayIndex < 0 || arrayIndex >= mData.Count) {
			return null;
		}
		return mData [arrayIndex];
	}
}
