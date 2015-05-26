
using System;
using System.Collections.Generic;
public class AchievementDetection
{
	private Dictionary<GameEvent, BoolDelegate_E_Args> mEvtDic = new Dictionary<GameEvent, BoolDelegate_E_Args> ();
	private AchievementData mData = null;

	public void Init(AchievementData data)
	{
		mData = data;
		mData.RegisterAllEvents ();
	}

	public void RegisterEvent (GameEvent eve, BoolDelegate_E_Args eventHandler)
	{
		mEvtDic [eve] += eventHandler;
	}
}