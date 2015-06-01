using UnityEngine;
using System.Collections;

public class DistortRing : MonoBehaviour {
	public float targetScale = 60f;
	public float fromDistort = 80f;
	public float lifeTime	 = 0.8f;
	private Material mat = null;
	private float mTimeCounter = 0f;
	private float mFrameRate = 24f;
	void Start () {
		mat = this.GetComponent<Renderer> ().material;
		mat.SetFloat ("_BumpAmt", fromDistort);
		StartCoroutine (DoScale());
	}

	IEnumerator DoScale()
	{
		int frameCount = Mathf.CeilToInt( lifeTime * mFrameRate );
		float totalTime = (frameCount - 1f) * 1f / mFrameRate;


		yield return null;

		for (int i = 0; i < frameCount; i++) 
		{
			mTimeCounter = i * 1f / mFrameRate;
			float mCurrentScale = Mathf.Lerp (0, targetScale, mTimeCounter / totalTime);
			transform.localScale = new Vector3 (mCurrentScale, mCurrentScale, 1f);
			float mCurrentDistort = Mathf.Lerp (fromDistort, 0, mTimeCounter / totalTime);
//			mat.SetFloat ("_BumpAmt", mCurrentDistort);
			yield return new WaitForSeconds(1f / mFrameRate);
		}

		Destroy(gameObject);

		yield break;
	}
}
