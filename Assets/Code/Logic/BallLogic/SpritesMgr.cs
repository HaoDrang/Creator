/*just do it in a simple way
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpritesMgr : MonoBehaviour
{
	[SerializeField]
	private Sprite[] preData;
	private Dictionary<string, Sprite> mDic = new Dictionary<string, Sprite>();

	public static SpritesMgr Instance{ get{return mInstance;} }
	private static SpritesMgr mInstance = null;
	void Awake()
	{
		foreach (var item in preData) {
			mDic[item.name] = item;
		}
		mInstance = this;
	}

//	void logException (string condition, string stackTrace, LogType type)
//	{

//	}

	public Sprite GetImage (string key)
	{
		Sprite sp = null;
		mDic.TryGetValue (key, out sp);
		return sp;
	}
}

