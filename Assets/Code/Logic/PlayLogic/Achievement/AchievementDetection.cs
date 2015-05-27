
using System;
using System.Collections.Generic;
public class AchievementDetection
{
	private Dictionary<GameEvent, BoolDelegate_E_Args> mEvtDic = new Dictionary<GameEvent, BoolDelegate_E_Args> ();
	private AchievementData mData = null;

	public void Init(AchievementData data)
	{
		mData = data;
		mData.RegisterAllEvents (this);
	}

	public void RegisterEvent (GameEvent eve, BoolDelegate_E_Args eventHandler)
	{
		if (!mEvtDic.ContainsKey (eve)) {
			mEvtDic.Add (eve, eventHandler);
		} else {
			mEvtDic[eve] += eventHandler;
		}
	}

	public void FireEvent (GameEvent eventType, params object[] args)
	{
		BoolDelegate_E_Args eventFunctionList = null;
		mEvtDic.TryGetValue (eventType, out eventFunctionList);
		if (eventFunctionList != null) {
			eventFunctionList(eventType, args);
		}
	}
}