using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ImagePartical : MonoBehaviour
{
	protected Image ParticalImage = null;
	protected bool  Loop = false;
	protected float LifeTime = 0f;
	protected float Delay = 0f;
	protected float DeltaAngle = 0f;
	protected AnimationCurve ScaleDuringTime = null;
	protected AnimationCurve AlphaDuringTime = null;
	protected Color	mColorFrom = default(Color);
	protected Color mColorTo = default(Color);
	protected float mTimeCounter = 0f;
	protected Vector3 mTargetScale = Vector3.one;
	protected bool mbPlay = false;
	protected Transform mTrans = null;

	void Start ()
	{
		mTrans = transform;
		mTargetScale = mTrans.localScale;
		mbPlay = true;

		// Cross Fade Color

	}
	
	// Update is called once per frame
	void Update ()
	{
		if (mbPlay) {
			if (Loop) {
				if (mTimeCounter < LifeTime) {
					mTimeCounter += Time.deltaTime;					
				} else {
					mTimeCounter = 0;
				}
			} else {
				if (mTimeCounter < LifeTime) {
					mTimeCounter += Time.deltaTime;
				} else {
					ParticalEnd ();
				}
			}
			SetScale (mTimeCounter, LifeTime);
		}

	}

	void SetScale (float curTime, float lifeTime)
	{
		float currentScale = ScaleDuringTime.Evaluate (curTime / lifeTime);
		mTrans.localScale = mTargetScale * currentScale;
	}

	void ParticalEnd ()
	{
		Destroy (gameObject);
	}
}
