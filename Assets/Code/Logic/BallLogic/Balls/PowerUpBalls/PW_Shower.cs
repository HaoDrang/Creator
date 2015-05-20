using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PW_Shower : PowerUpLogicBase
{
	const int miDropCount 		= 15;
	const float mfShootInterval = 0.1f;
	const float mfShootForce = 20f;
	const float mfWidthRange = 1;
	private GameObject coroutineStarter = null;
	private BoolDelegate_GObj_GObj mColliderCallback = null;
	public override void Shoot (UnityEngine.GameObject obj, UnityEngine.Transform p, 
	                            UnityEngine.Vector3 worldPos, UnityEngine.Quaternion worldRot, 
	                            UnityEngine.Vector2 velocity)
	{
		coroutineStarter = GameObject.Instantiate<GameObject>(obj);
		//disable all compnents
		coroutineStarter.GetComponent<Image>().enabled 		= false;
		coroutineStarter.GetComponent<Animator>().enabled 	= false;
		coroutineStarter.GetComponent<Collider2D>().enabled 		= false;
		coroutineStarter.GetComponent<Rigidbody2D>().isKinematic	= true;
		Ball b = coroutineStarter.GetComponent<Ball>();
		b.StartCoroutine(Shower(obj, p, worldPos, worldRot, velocity));
		//base.Shoot (obj, p, worldPos, worldRot, velocity);
	}

	IEnumerator Shower(UnityEngine.GameObject orginDrop, UnityEngine.Transform p, 
	                   UnityEngine.Vector3 worldPos, UnityEngine.Quaternion worldRot, 
	                   UnityEngine.Vector2 velocity)
	{
		//set shooter shooting
		orginDrop.SetActive(false);
		yield return null;

		GameObject shooter = GameObject.Find(BallDefines.SHOOTER_OBJECT_NAME);
		if (shooter == null) {
			Debug.LogError("There is no Object named : " + 
			               BallDefines.SHOOTER_OBJECT_NAME + "in the Scene");
			yield break;
		}

		BallShooter bs = shooter.GetComponent<BallShooter>();
		bs.PreventShooter = true;

		yield return null;

		for (int i = 0; i < miDropCount; i++) {
			GameObject obj = GameObject.Instantiate<GameObject>(orginDrop);
			obj.SetActive(true);
			base.Shoot (obj, p, worldPos, worldRot, GetRandomDirection() * mfShootForce);
			SpecialBall b = obj.GetComponent<SpecialBall>();
			b.SetColliderCallback(mColliderCallback);
			b.SetBallType(BallType.PowerUp);
			b.SetSpecialBallType(SpecialBallType.SHOWER);
			yield return new WaitForSeconds(mfShootInterval);
		}
		bs.PreventShooter = false;
		GameObject.Destroy(coroutineStarter);
		//set shooter shoot done
		yield break;
	}

	Vector2 GetRandomDirection()
	{
		float range = UnityEngine.Random.Range(-mfWidthRange / 2f,mfWidthRange / 2f);
		return new Vector2(range,1);
	}

	public BoolDelegate_GObj_GObj ColliderCallback {
		get {
			return mColliderCallback;
		}
		set {
			mColliderCallback = value;
		}
	}
	
	public override void HandleCollidOtherBall (BallDisposer mBallListController, 
	                                            GameObject shooted, GameObject stable)
	{
		if (stable != null) {
			Ball b = stable.GetComponent<Ball>();
			if (b != null) {
				b.DoFadeOut();
			}
			else
			{
				//Collided the borders
				//Debug.Log(stable.name);
			}
		}
		
		shooted.GetComponent<Ball>().DoFadeOut();
		Rigidbody2D rb = shooted.GetComponent<Rigidbody2D>();
		rb.velocity = Vector2.zero;
		rb.gravityScale = 0.5f;
	}
}