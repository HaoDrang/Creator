using System;
using System.Collections.Generic;
using UnityEngine;
public class AchievementData
{
	Dictionary<string,SingleAchievement> mAchievements = new Dictionary<string,SingleAchievement>();
	Dictionary<string,bool>		mDoneList = new Dictionary<string, bool>();
	const string AchievementKey 		= "achievement";
	const string AchievementCountKey 	= "count";

	public void Init(Property prop)
	{
		string key = AchievementKey + "." + AchievementCountKey;

		int count = prop.GetInt (key);

		CreateAchievements (prop, count);
	}

	void CreateAchievements (Property prop, int iCount)
	{
		for (int i = 0; i < iCount; i++) 
		{
			SingleAchievement sa = new SingleAchievement();
			sa.Init(prop, i);
			mAchievements.Add(sa.KeyName, sa);
			bool finished = false;
			finished = PlayerPrefs.GetInt(sa.KeyName/* + Player GUID*/, 0) > 0;
			mDoneList.Add(sa.KeyName, finished);
		}
	}

	public void RegisterAllEvents (AchievementDetection adt)
	{
		foreach (var item in mAchievements) {
			if (item.Value != null) {
				item.Value.RegisterEventHandlers(adt);
			}
		}
	}
}