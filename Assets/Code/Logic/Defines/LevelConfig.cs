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
	public SpecialBallType[] meSuperBallTypes;
	public int	 	mEmptyOdds 			= 0;//0-100
	public int		mMoveDownCount 		= 8;
	public int 		mMatchingBallCount 	= 3;
	public int 		mPushProgress 		= 5;
	public int[] mPowerUpDelayInitial 	= new int[2];
	public int[] mPowerUpDelay			= new int[2];
	public LevelEnum mDifficult = LevelEnum.Beginner;

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
	const string SpecialBallKey			= "powertypes";

	public static LevelConfig[] LoadLevelConfigs(Property prop)
	{
        LevelConfig[] lcArray = new LevelConfig[(int)LevelEnum.LevelNum];
		string key = "";
		for (int i = 0; i < (int)LevelEnum.LevelNum; i++) {
			key = LevelConfigKey + "." + ((LevelEnum)i).ToString().ToLower() + ".";
			LevelConfig lc = new LevelConfig();
			lc.levelName = prop.GetString(key + LevelNameKey);
			lc.mWidth = prop.GetIntArray(key + WidthKey);
			lc.miColorCount = prop.GetInt(key + ColorCountKey);
			lc.mEmptyOdds = prop.GetInt(key + EmptyOddsKey);
			lc.mMoveDownCount = prop.GetInt(key + MoveDownCountKey);
			lc.mMatchingBallCount = prop.GetInt(key + MatchingBallCountKey);
			lc.mPushProgress = prop.GetInt(key + PushProgressKey);
			lc.mPowerUpDelayInitial = prop.GetIntArray(key + PowerUpDelayInitialKey);
			lc.mPowerUpDelay = prop.GetIntArray(key + PowerUpDelay);
			lc.mDifficult = (LevelEnum)i;
			lc.meSuperBallTypes = Array.ConvertAll<int,SpecialBallType>(
				prop.GetIntArray(key + SpecialBallKey),
				iInput => (SpecialBallType)iInput);

			lcArray[i] = lc;
		}
		return lcArray;
	}
}