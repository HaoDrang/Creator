using System;
using UnityEngine;
public class PW_ChainSaw : PowerUpLogicBase
{
	const float r = 5f;
	const float force = 30f;
	public override void Shoot (UnityEngine.GameObject obj, UnityEngine.Transform p, UnityEngine.Vector3 worldPos, 
	                            UnityEngine.Quaternion worldRot, UnityEngine.Vector2 velocity)
	{
		base.Shoot (obj, p, worldPos, worldRot, velocity);
		CircleCollider2D col2d = obj.GetComponent<CircleCollider2D>();
		col2d.isTrigger = true;
		col2d.radius = r;
	}

	public override void HandleCollidOtherBall (BallDisposer mBallListController, 
	                                            UnityEngine.GameObject shooted, 
	                                            UnityEngine.GameObject stable)
	{
		if (stable.tag.Contains(BallDefines.BORDER_KEY_WORD)) 
		{
			//TODO play a destroy effect here
			GameObject.Destroy(shooted);
		}
		BombOut(stable, force*(stable.transform.position - shooted.transform.position));
	}

	void BombOut(UnityEngine.GameObject target, Vector3 speed)
	{
		Ball b = target.GetComponent<Ball> ();
		if (b != null) 
		{
			target.layer = Layers.FallingBall;
			Rigidbody2D rb2d = target.GetComponent<Rigidbody2D> ();
			rb2d.isKinematic = false;
			rb2d.gravityScale = 1f;
			rb2d.velocity = new Vector2 (speed.x, speed.y);
			b.DoFadeOut ();
		}
	}

	public override bool HandleColledBorder (GameObject ballObject, GameObject border)
	{
		ballObject.GetComponent<Ball>().ShootOut();
		return true;
	}
}