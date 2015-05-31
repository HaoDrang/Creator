using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DropText : MonoBehaviour {
	[SerializeField]
	private Text countWords = null;
	private int miCount = 0;
	[SerializeField]
	private Animator anm = null;
	[SerializeField]
	private AnimationEventDispatcher aed = null;
	[SerializeField]
	private float TargetWaitForSeconds = 0.8f;
	const string ShowStr = "DropCountShow";
	const string EndStr = "DropCountFade";
	void Start()
	{
		aed.mShowEndCallBack += ShowEnd;
		aed.mFadeEndCallBack += FadeEnd;

		countWords.text = miCount.ToString();
		
		anm.Play(ShowStr);
	}
	
	public void Init(int iCount)
	{
		miCount = iCount;
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
