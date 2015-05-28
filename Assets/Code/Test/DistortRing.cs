using UnityEngine;
using System.Collections;

public class DistortRing : MonoBehaviour {
	public float targetScale = 60f;
	public float fromDistort = 80f;
	public float lifeTime	 = 0.8f;
	private Material mat = null;
	private float mTimeCounter = 0f;
	void Start () {
		mat = this.GetComponent<Renderer> ().material;
	}
	
	void Update () {
		mTimeCounter += Time.deltaTime;
		if (mTimeCounter >= lifeTime) {
			Destroy(gameObject);
		}
		float mCurrentScale = Mathf.Lerp (0, targetScale, mTimeCounter / lifeTime);
		transform.localScale = new Vector3 (mCurrentScale, mCurrentScale, 1f);
		float mCurrentDistort = Mathf.Lerp (fromDistort, 0, mTimeCounter / lifeTime);
		mat.SetFloat ("_BumpAmt", mCurrentDistort);
	}
}
