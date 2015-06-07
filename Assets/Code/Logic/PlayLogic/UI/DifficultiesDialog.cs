using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DifficultiesDialog : MonoBehaviour {
	private LevelEnum mDifficult = default(LevelEnum);
	private Main mMain = null;
	void Start()
	{
		GameObject obj = GameObject.Find(BallDefines.MAIN_OBJECT_NAME);
		mMain = obj.GetComponent<Main>();
	}
	public void OnToggleCheck(int i)
	{
		mDifficult = (LevelEnum)i;
		mMain.SetDifficult(mDifficult);
	}

	public void OnGameStart()
	{
		mMain.GameBegin();
	}
}
