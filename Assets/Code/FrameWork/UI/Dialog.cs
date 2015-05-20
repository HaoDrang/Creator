using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dialog : MonoBehaviour 
{
	protected string key = "";
	[SerializeField]
	protected GameObject 	mButtonPrefab 	= null;
	[SerializeField]
	protected GameObject	mTextPrefab 	= null;
	[SerializeField]
	protected GameObject	mImagePrefab	= null;
	protected bool   mUIReady		= false;


	//-------------------------------------------------------------
	protected const string ScaleKey 	= "scale";
	protected const string PositionKey 	= "position";
	protected const string AnchorKey		= "anchor";
	protected const string Min			= "min";
	protected const string Max			= "max";
	protected const string FontSizeKey 	= "fontsize";
	protected const string FontStyleKey = "fontstyle";
	protected const string ContentKey 	= "content";
	//-------------------------------------------------------------

	public void Init(string k)
	{
		key = k;
	}
	// Use this for initialization
	IEnumerator Start () 
	{
		CanvasRenderer cr = GetComponent<CanvasRenderer>();
		cr.SetAlpha(0);
		yield return null;
		// build UI
		BuildUI();
		yield return null;
		cr.SetAlpha(1);
		mUIReady = true;
		yield break;
	}

	virtual protected void BuildUI()
	{
		RectTransform rt = GetComponent<RectTransform>();
		ScaleAndPosition(rt, key);
	}

	public Button CreateButton(string buttonKey)
	{
		if (mButtonPrefab != null) {
			string fullBtnKey = key + "." + buttonKey;
			GameObject obj = GameObject.Instantiate<GameObject>(mButtonPrefab.gameObject);

			RectTransform rt = obj.GetComponent<RectTransform>();
			rt.SetParent(transform);
			rt.localScale = Vector3.one;
			ScaleAndPosition(rt, fullBtnKey);

			// set button sprite
			obj.SetActive(true);
			return obj.GetComponent<Button>();
		}
		else
		{
			Debug.Log("Error Dialog Button Prefab Lost!");
			return null;
		}
	}

	public Text GreateText(string textKey)
	{
		if (mTextPrefab != null) {
			string fullTextKey = key + "." + textKey;
			GameObject obj = GameObject.Instantiate<GameObject>(mTextPrefab.gameObject);
			obj.transform.localScale = Vector3.one;
			RectTransform rt = obj.GetComponent<RectTransform>();
			rt.localScale = Vector3.one;
			ScaleAndPosition(rt, fullTextKey);
			obj.SetActive(true);

			Text t = obj.GetComponent<Text>();
			t.fontSize = Property.GetInt(fullTextKey + "." + FontSizeKey);
			t.fontStyle = (FontStyle)Property.GetInt(fullTextKey + "." + FontStyleKey);
			t.text = Property.GetString(fullTextKey + "." + ContentKey);

			return t;
		}	
		else
		{
			Debug.Log("Error Dialog Text Prefab Lost!");
			return null;
		}
	}

	//TODO MAKE A SPrite loader

	protected void ScaleAndPosition (RectTransform rt, string fullBtnKey)
	{
		// change Scale
		Vector2 size = Property.GetVector2(fullBtnKey + "." + ScaleKey);
		rt.sizeDelta = size;

		// change Position
		Vector2 pos = Property.GetVector2(fullBtnKey + "." + PositionKey);
		rt.localPosition = pos;


		Vector2 acMin = Property.GetVector2(fullBtnKey + "." + AnchorKey + "." + Min);
		Vector2 acMax = Property.GetVector2(fullBtnKey + "." + AnchorKey + "." + Max);

		rt.anchorMin = acMin;
		rt.anchorMax = acMax;
	}
}
