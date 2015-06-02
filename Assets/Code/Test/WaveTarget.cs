using UnityEngine;
using System.Collections;

public class WaveTarget : MonoBehaviour {
	private Transform mTrans = null;
	[SerializeField]
	private AnimationCurve mHC;
	[SerializeField]
	private AnimationCurve mVC;
	[SerializeField]
	private AnimationCurve mDHc;
	[SerializeField]
	private AnimationCurve mDVc;
	[SerializeField]
	private float during = 1f;
	[SerializeField]
	private float maxHScale = 1f;
	[SerializeField]
	private float maxVScale = 1f;
	[SerializeField]
	private float maxHDistance = 20f;
	[SerializeField]
	private float maxVDistance = 20f;
	private TimeCounter mCounter = null;
	private Vector3 mvOldLocalPos 	= default(Vector3);
	private Vector3 mvOldScale 		= default(Vector3);
	// Use this for initialization
	void Start () 
	{
		mCounter 	= new TimeCounter(during, LerpScale, WaveDone);
		mTrans 		= transform;
		mvOldLocalPos 	= mTrans.localPosition;
		mvOldScale 		= mTrans.localScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		mCounter.Tick(Time.deltaTime);
	}

	void LerpScale (float curT, float during)
	{
		float k = curT / during;
		float hScale = mHC.Evaluate(k) * maxHScale;
		float vScale = mVC.Evaluate(k) * maxVScale;
		float offsetx = mDHc.Evaluate(k) * maxHDistance;
		float offsety = mDVc.Evaluate(k) * maxVDistance;

		Vector3 scale 	= new Vector3(hScale, vScale, 1);
		Vector3 pos 	= mvOldLocalPos;
		pos.x += offsetx;
		pos.y += offsety;

		mTrans.localScale 		= scale;
		mTrans.localPosition 	= pos;
	}

	void WaveDone ()
	{
		mCounter.Reset();
	}

	public void Play()
	{
		mCounter.Play();
	}

	public void Stop()
	{
		mCounter.Stop();
	}

	public void Reset()
	{
		mCounter.Reset();
		mTrans.localScale 		= mvOldScale;
		mTrans.localPosition 	= mvOldLocalPos;
	}
}
