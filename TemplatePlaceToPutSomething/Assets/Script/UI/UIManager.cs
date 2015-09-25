using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour {

	public static UIManager mInstance = null;
	public static UIManager Instance
	{
		get{
			return mInstance;
		}
	}

	private UIBase[] mUIs;

	private IList<FileParser> mPasers = null;

	void Awake(){
		mInstance = this;

		init();
	}

	void init ()
	{
		InitParser ();
	}

	void InitParser ()
	{
		mPasers = new List<FileParser> ();

		mPasers.Add (new FilePaserExcel(this));
	}

	void OnDestroy()
	{
		mInstance = null;
	}

	public FileParser Parser
	{
		get{
			return mPasers[PARSER.EXCEL];
		}
	}

	public T GetUI<T>(UIEnum idx) where T:UIBase
	{
		return (T)(mUIs [(int)idx]);
	}

	public UIBase GetUI(UIEnum idx)
	{
		return (UIBase)(mUIs [(int)idx]);
	}
}

public class PARSER{
	public const int EXCEL = 0;
}