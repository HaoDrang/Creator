using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultiesDialog : MonoBehaviour {
	private LevelEnum mDifficult = default(LevelEnum);
	private Main mMain = null;
	private const string DifficultKey = "difficulty";

	void Start()
	{
		GameObject obj = GameObject.Find(BallDefines.MAIN_OBJECT_NAME);
		mMain = obj.GetComponent<Main>();
		mDifficult = (LevelEnum)PlayerPrefs.GetInt (DifficultKey);
		OnToggleCheck ((int)mDifficult);
	}

	public void OnToggleCheck(int i)
	{
		mDifficult = (LevelEnum)i;
		mMain.SetDifficult(mDifficult);
		PlayerPrefs.SetInt (DifficultKey, i);
		PlayerPrefs.Save ();
	}

	public void OnGameStart()
	{
		mMain.GameBegin();
	}
}
