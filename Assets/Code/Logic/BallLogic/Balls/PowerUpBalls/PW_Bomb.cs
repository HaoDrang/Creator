using System;
using UnityEngine;

public class PW_Bomb : PowerUpLogicBase
{
	const float mForce = 10f;
	const float mDropTime = 1f;
	public override void HandleCollidOtherBall (BallDisposer mBallListController, UnityEngine.GameObject shooted, UnityEngine.GameObject stable)
	{
		//remove balls next to it
		Ball hitted = stable.GetComponent<Ball>();
		Ball[] balls = mBallListController.GetNeighbourBalls (hitted, 1);

		BombOut(stable, mForce * Vector3.down);
		if (balls != null) {
			for (int i = 0; i < balls.Length; i++) {
				GameObject obj = balls[i].gameObject;
				Vector3 speed = mForce * (obj.transform.position - stable.transform.position);
				BombOut(obj, speed);
			}
		}
		GameObject.Destroy (shooted);

		// Create an  explosion here
	}

	void BombOut(UnityEngine.GameObject target, Vector3 speed)
	{
		Ball b = target.GetComponent<Ball> ();
		if (b != null) {
			target.layer = Layers.FallingBall;
			Rigidbody2D rb2d = target.GetComponent<Rigidbody2D> ();
			rb2d.isKinematic = false;
			rb2d.gravityScale = 2f;
			rb2d.velocity = new Vector2 (speed.x, speed.y);
			b.DoFadeOut ();
		}
	}
}