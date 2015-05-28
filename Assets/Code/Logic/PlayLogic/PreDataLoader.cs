using UnityEngine;
using System.Collections;

public class PreDataLoader : MonoBehaviour 
{
	[SerializeField]
	private string PropertyPath = "";
	[SerializeField]
	private int TargetFrameRate = 10;
	public GameObject[] ToBeEnabled;

	void Awake()
	{
		Application.targetFrameRate = TargetFrameRate;
		for (int i = 0; i < ToBeEnabled.Length; i++) {
			GameObject obj = ToBeEnabled[i];
			if (obj != null) {
				obj.SetActive(false);
			}
		}
	}

	IEnumerator Start()
	{
		// above all
		Property.Instante.LoadDictionary(PropertyPath);
		Property props = Property.Instante;
		yield return null;
		// init managers
		ColorBoard.Init (props);
		// init Achievements
		AchievementData achievementdata = new AchievementData ();
		achievementdata.Init (props);
		yield return null;
		AchievementDetection adetection = new AchievementDetection ();
		adetection.Init (achievementdata);
		yield return null;
		GameObject mainObj = ToBeEnabled[0];
		Main mainScript = mainObj.GetComponent<Main> ();
		mainScript.AchievementDetector = adetection;
		yield return null;
		for (int i = 0; i < ToBeEnabled.Length; i++) {
			GameObject obj = ToBeEnabled[i];
			if (obj != null) {
				obj.SetActive(true);
			}
		}
		yield break;
	}
}
