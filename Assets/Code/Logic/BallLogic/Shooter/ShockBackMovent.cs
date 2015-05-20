using UnityEngine;
using System.Collections;

public class ShockBackMovent : MonoBehaviour {
	[SerializeField]
	private float MovementScale = 20f;
	[SerializeField]
	private float ShockTime = 0.3f;
	[SerializeField]
	private AnimationCurve Curve;

	private bool mAnimating = false;
	private float mTimeCounter = 0f;
	private Vector3 mOldPosition = default(Vector3);
	private VoidDelegate mShootEndCallBack = null;
	public bool Cooling{ get{ return mAnimating; } }

	void Update () 
	{
		if (mAnimating && (mTimeCounter > 0f)) {
			mTimeCounter -= Time.deltaTime;
			DoAnimate(mTimeCounter);
		} else {
			mTimeCounter = 0;
			mAnimating = false;
			AnimationEnd();
		}
	}

	public void Shock(VoidDelegate cb = null)
	{
		if (mAnimating) {
			return;
		}
		mShootEndCallBack = cb;
		mAnimating = true;
		mOldPosition = transform.localPosition;
		mTimeCounter = ShockTime;
	}

	void AnimationEnd ()
	{
		if (mShootEndCallBack != null) {
			mShootEndCallBack();
		}
	}

	void DoAnimate (float mTimeCounter)
	{
		Vector3 pos = mOldPosition;
		pos.y -= Curve.Evaluate (1f - mTimeCounter / ShockTime) * MovementScale;
		transform.localPosition = pos;
	}
}
