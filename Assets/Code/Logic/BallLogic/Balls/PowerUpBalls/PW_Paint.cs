using System;
using UnityEngine;
public class PW_Paint : PowerUpLogicBase
{
	const int radius = 1;
	public override void HandleCollidOtherBall (BallDisposer mBallListController, 
	                                            UnityEngine.GameObject shooted, 
	                                            UnityEngine.GameObject stable)
	{
		if (stable != null) 
		{
			Ball b = stable.GetComponent<Ball>();
			if (b != null) {
				// change the middle ball into a regular color ball
				switch (b.GetBallType()) {
				case BallType.Regular:
					// do nothing
					break;
				case BallType.PowerUp:
					ChangeBallToRegular(ref b, mBallListController.GetRandBallColor());
					break;
				default:
					break;
				}

				RegularBall midBall = b as RegularBall;

				Ball[] balls = mBallListController.GetNeighbourBalls(b, radius);
				for (int i = 0; i < balls.Length; i++) {
					Ball eachB = balls[i];
					switch (eachB.GetBallType()) {
					case BallType.PowerUp:
						ChangeBallToRegular(ref eachB, midBall.GetColor());
						break;
					case BallType.Regular:
						RegularBall rb = eachB as RegularBall;
						rb.SetBallColor(midBall.GetColor());
						break;
					default:
					break;
					}
				}
			}
		}

		// TODO Create a big animation for painter from middle

		//do old ball fade out
		Ball shootBall = shooted.GetComponent<Ball>();
		shootBall.DoFadeOut();
		Rigidbody2D rd2d = shootBall.GetComponent<Rigidbody2D>();
		rd2d.velocity = Vector2.zero;

	}

	void ChangeBallToRegular (ref Ball b, int ballColor)
	{
		Ball newBall = RegularBall.Create(b.GetBallRow(), ballColor);
		b.GetBallRow().SetBall(newBall, b.GetIndex());
		b = newBall;
	}
}