using UnityEngine;
using System.Collections;

public class AnimationEventDispatcher : MonoBehaviour 
{
	public VoidDelegate mShowEndCallBack = null;
	public VoidDelegate mFadeEndCallBack = null;

	public void ShowEnd()
	{
		if (mShowEndCallBack != null) {
			mShowEndCallBack();
		}
	}

	public void FadeEnd()
	{
		if (mFadeEndCallBack != null) {
			mFadeEndCallBack();
		}
	}
}
