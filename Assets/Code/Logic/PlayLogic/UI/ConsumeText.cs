using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ConsumeText : MonoBehaviour 
{
	[SerializeField]
	private Text countWords = null;
	[SerializeField]
	private Animator anm = null;
	[SerializeField]
	private AnimationEventDispatcher aed = null;
	private int miCount = 0;
	private Color mcColor = default(Color);
	[SerializeField]
	private float TargetWaitForSeconds = 0.6f;
	const string ShowStr = "ConsumeCountShow";
	const string EndStr = "ConsumeCountFlyDown";
	void Start()
	{
		aed.mShowEndCallBack += ShowEnd;
		aed.mFadeEndCallBack += FadeEnd;
		countWords.text = miCount.ToString();
		countWords.color = mcColor;

		anm.Play(ShowStr);
	}

	public void Init(int iCount, Color c)
	{
		miCount = iCount;
		mcColor = c;
	}

	public void ShowEnd()
	{
		Invoke ("WaitDone", TargetWaitForSeconds);
	}

	void WaitDone()
	{
		anm.Play(EndStr);
	}

	public void FadeEnd()
	{
		Destroy (gameObject);
	}
}
