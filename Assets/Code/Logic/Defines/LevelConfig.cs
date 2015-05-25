using System;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class LevelConfig
{
	public string 	levelName = "";
	public int[] 	mWidth;
	public int 		miColorCount = 3;
	public Color[] 	mColors;
	public SpecialBallType[] eTypes;
	public int	 	mEmptyOdds 			= 0;//0-100
	public int		mMoveDownCount 		= 8;
	public int 		mMatchingBallCount 	= 3;
	public int 		mPushProgress 		= 5;
	public int[] mPowerUpDelayInitial 	= new int[2];
	public int[] mPowerUpDelay			= new int[2];

	public LevelConfig CreateNewConfig(Color[] cArray)
	{
		//init colors
		mColors = new Color[miColorCount];
		ArrayTool.Shuffle<Color> (cArray);
		for (int i = 0; i < miColorCount; i++) 
		{
			mColors[i] = cArray[i];
		}

		//init ball types

		return this;
	}

	const string LevelConfigKey 		= "levelconfig";
	const string LevelNameKey 			= "levelname";
	const string WidthKey 				= "width";
	const string ColorCountKey 			= "colorcount";
	const string EmptyOddsKey 			= "emptyodds";
	const string MoveDownCountKey 		= "movedowncount";
	const string MatchingBallCountKey 	= "matchingballcount";
	const string PushProgressKey 		= "pushprogress";
	const string PowerUpDelayInitialKey = "powerupdelayinitial";
	const string PowerUpDelay 			= "powerupdelay";

	public static LevelConfig[] LoadLevelConfigs()
	{
        LevelConfig[] lcArray = new LevelConfig[(int)LevelEnum.LevelNum];
		string key = "";
		for (int i = 0; i < (int)LevelEnum.LevelNum; i++) {
			key = LevelConfigKey + "." + ((LevelEnum)i).ToString().ToLower() + ".";
			LevelConfig lc = new LevelConfig();
			lc.levelName = Property.GetString(key + LevelNameKey);
			lc.mWidth = Property.GetIntArray(key + WidthKey);
			lc.miColorCount = Property.GetInt(key + ColorCountKey);
			lc.mEmptyOdds = Property.GetInt(key + EmptyOddsKey);
			lc.mMoveDownCount = Property.GetInt(key + MoveDownCountKey);
			lc.mMatchingBallCount = Property.GetInt(key + MatchingBallCountKey);
			lc.mPushProgress = Property.GetInt(key + PushProgressKey);
			lc.mPowerUpDelayInitial = Property.GetIntArray(key + PowerUpDelayInitialKey);
			lc.mPowerUpDelay = Property.GetIntArray(key + PowerUpDelay);
			lcArray[i] = lc;
		}
		return lcArray;
	}
}