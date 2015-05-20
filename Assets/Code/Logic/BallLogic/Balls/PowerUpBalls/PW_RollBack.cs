using System;
using UnityEngine;

public class PW_RollBack : PowerUpLogicBase
{
	public override void Shoot (UnityEngine.GameObject obj, UnityEngine.Transform p, UnityEngine.Vector3 worldPos, UnityEngine.Quaternion worldRot, UnityEngine.Vector2 velocity)
	{
		base.Shoot (obj, p, worldPos, worldRot, velocity);
	}

	public override void HandleCollidOtherBall (BallDisposer mBallListController, UnityEngine.GameObject shooted, UnityEngine.GameObject stable)
	{
		Rigidbody2D rd = shooted.GetComponent<Rigidbody2D>();
		rd.velocity = Vector3.zero;
		rd.gravityScale = 1f;
		shooted.GetComponent<Ball>().DoFadeOut();
		mBallListController.RollBack(1);
	}
}