using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrefabMgr : MonoBehaviour 
{
	[SerializeField]
	private GameObject[] preData;
	private Dictionary<string, GameObject> mDic = new Dictionary<string, GameObject>();
	
	public static PrefabMgr Instance{ get{return mInstance;} }
	private static PrefabMgr mInstance = null;
	void Awake()
	{
		foreach (var item in preData) {
			mDic[item.name] = item;
		}
		mInstance = this;
	}
	
	public GameObject GetPrefab (string key)
	{
		GameObject sp = null;
		mDic.TryGetValue (key, out sp);
		return sp;
	}

	public GameObject CreateCopy(string key)
	{
		GameObject obj = GetPrefab (key);
		if (obj != null) 
		{
			obj = Instantiate<GameObject>(obj);
			return obj;
		}
		return null;
	}
}
