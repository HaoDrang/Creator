using UnityEngine;

public class SingleAchievement
{
	protected bool 		mbDone 			= false;
	protected int		miID 			= 0;
	protected string 	msDisplayName 	= "";
	protected string 	msKeyName 		= "";
	protected int[] 	miaEvents 	= null;
	protected int[]		miaEventsMax = null;

	protected const string MainKey 			= "achievement";
	protected const string KeyNameKey 		= "keyname";
	protected const string DisplayNameKey 	= "displayname";
	protected const string EventsKey 		= "event";
	protected const string EventsMaxKey 	= "eventmax";

	virtual public void Init(Property prop, int id)
	{
		miID = id;

		string key = MainKey + "." + miID.ToString ();

		// load key name
		string tempKey = key + "." + KeyNameKey;
		msKeyName = prop.GetString (tempKey);
		// load dislayname
		tempKey = key + "." + DisplayNameKey;
		msDisplayName = prop.GetString (tempKey);
		// load condition list
		tempKey = key + "." + EventsKey;
		miaEvents = prop.GetIntArray (tempKey);
		// load event max count
		tempKey = key + "." + EventsMaxKey;
		miaEventsMax = prop.GetIntArray (tempKey);

		Debug.Log (miID.ToString() + "-" + msKeyName + "-" + msDisplayName);
	}

	virtual public void RegisterEventHandlers(AchievementDetection adt)
	{
		adt.RegisterEvent (GameEvent.BallCollide, EventHandler);
	}

	virtual public bool EventHandler(GameEvent eEventType, params object[] args)
	{
		return true;
	}

	virtual public void ClearAll()
	{

	}

	virtual public void ClearRound()
	{

	}

	virtual public void ClearGame()
	{

	}

	public string KeyName 
	{
		get{
			return msKeyName;
		}
	}
}