using UnityEngine;
using System.Collections;

public class BackgroundSky : BackgroundLayerBase
{
	private ColorSetter[] SkyBase;
	private ColorSetter[] SkyDecorator;
	private ColroSetter[] SkyVeins;
	[SerializeField]
	private LevelEnum mLevelLimit_1;
	[SerializeField]
	private LevelEnum mLevelLimit_2;
	//TODO
	public override void BuildLayer (LevelConfig conf)
	{
		if (SkyBase.Length > 0) {
			// Select an sky base
			// set color of it
		}


		if (conf.mDifficult >= mLevelLimit_1) 
		{
			if (SkyDecorator.Length > 0) 
			{
				//Select an sky decoration
				// set color of it
			}
		}

		if (conf.mDifficult >= mLevelLimit_2) 
		{
			if (SkyVeins.Length > 0) {
				// Select an Sky veins
				// set color of it
			}
		}


	}
}
