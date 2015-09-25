using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class FileParser 
{
	public TextAsset text = null;

	private const int MAX_LINE_NUM = 8096;

	private const string DATA_CALL_BACK_FUNC = "RecieveMessage";

	protected IDictionary<string, string> mDic = new Dictionary<string, string>();

	private Behaviour mDataCallBackTarget = null;

	private bool mbProcessingLock = false;

	private MonoBehaviour mCoDependent = null;

	public FileParser(MonoBehaviour obj)
	{
		mCoDependent = obj;
	}

	public void RequireLines(Behaviour callBackTarget)
	{
		if (text == null)
			return;

		if (mbProcessingLock) {
			if(callBackTarget != null){
				Debug.Log ("Noop! the Parser is Processing, please try later. Required from:" + callBackTarget.name +
			    	       " (Attached at " + callBackTarget.gameObject.name + " )");
			}
			else{
				Debug.Log("Noop! the Parser is Processing, please try later. Please take a look at stack:" + 
				          (new System.Diagnostics.StackTrace()).GetFrame(0).ToString());
			}
			return;
		}

		mbProcessingLock = callBackTarget;

		StreamReader sr = new StreamReader(new MemoryStream(text.bytes));

		mCoDependent.StartCoroutine (ReadFlow(sr));
	}
	
	IEnumerator ReadFlow (StreamReader sr)
	{
		for (int i = 0; i < MAX_LINE_NUM; i++) {

			string str = sr.ReadLine();
			if(str != null){
				//Debug.Log(str);
				ParseLine(str);
			}
			else{
				break;
			}
			yield return null;
		}

		// load Done and send message callback
		mDataCallBackTarget.SendMessage (DATA_CALL_BACK_FUNC, mDic, SendMessageOptions.DontRequireReceiver);

		mbProcessingLock = false;
		yield return null;
	}

//	void Update()
//	{
//		if (Input.GetKeyDown (KeyCode.Space)) {
//			RequireLines(this);
//		}
//	}

	virtual protected void ParseLine(string str){

	}

	protected void SetDic(string key, string value)
	{
		if (mDic.ContainsKey (key)) {
			//Debug.LogWarning ("Conflicts ! ---> " + key);
			mDic [key] = value;
		} else {
			mDic.Add(key, value);
		}

		//Debug.Log ("key:" + key + "<><>" + "value:" + value);
	}
}
