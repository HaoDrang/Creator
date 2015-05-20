using System;
using UnityEngine;
[Serializable]
public class LevelConfig
{
	public int[] mWidth;
	public Color[] Colors;
	public SpecialBallType[] eTypes;
	public int	 mEmptyOdds = 0;//0-100
	public int	mMoveDownCount = 8;
	public int mMatchingBallCount = 3;
	public int mPushProgress = 5;
	public int[] mPowerUpDelayInitial 	= new int[2];
	public int[] mPowerUpDelay			= new int[2];
}