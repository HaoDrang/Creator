public class SingleAchievement
{
	protected bool 		mbDone 			= false;
	protected int		miID 			= 0;
	protected string 	msDisplayName 	= "";
	protected string 	msKeyName 		= "";
	protected string[] 	msaConditions 	= null;

	protected const string MainKey = "achievement";
	protected const string KeyNameKey = "keyname";
	protected const string DisplayNameKey = "displayname";
	protected const string ConditionsKey = "conditions";

	virtual public void LoadBaseAchievement(int id, AchievementDetection adetection, Property prop)
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
		tempKey = key + "." + ConditionsKey;
		msaConditions = prop.GetStringArray (tempKey);

	}
}