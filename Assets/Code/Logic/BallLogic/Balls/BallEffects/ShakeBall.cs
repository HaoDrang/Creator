using UnityEngine;
using System.Collections;

public class ShakeBall : MonoBehaviour {

	protected bool  mbShake = false;
	protected Vector3 mvPrePos = Vector3.zero;
	[SerializeField]
	protected AnimationCurve macCurve;
	[SerializeField]
	protected float mfShakeTime = 0.5f;
	[SerializeField]
	protected float mShakeRange = 10f;
	protected float mfShakedTime = 0f;

	VoidDelegate mShakeCallBack = null;

	// Update is called once per frame
	void Update () {

		if (mbShake) {
			mfShakedTime += Time.deltaTime;
			if (mfShakedTime >= mfShakeTime) {
				ShakeDone();
			}
			else
			{
				// do shake
				float offset = macCurve.Evaluate(mfShakedTime / mfShakeTime) * mShakeRange;
				transform.position = mvPrePos + (new Vector3(offset,0,0));
			}
		}
	}

	public void Shake(VoidDelegate cb = null)
	{
		mvPrePos = transform.position;
		mbShake = true;
		mShakeCallBack = cb;
	}

	void ShakeDone ()
	{
		mbShake = false;
		transform.position = mvPrePos;
		if (mShakeCallBack != null) {
			mShakeCallBack();
		}
	}
}
