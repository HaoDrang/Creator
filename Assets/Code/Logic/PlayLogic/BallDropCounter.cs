using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class BallDropCounter : MonoBehaviour
{
	//send event to main
	[SerializeField]
	Main mMain = null;
	void OnTriggerEnter2D(Collider2D ball) 
	{
		Ball b = ball.GetComponent<Ball>();
		if (b != null) {
			switch (b.GetBallType()) {
			case BallType.Regular:
				RegularBall rb = b as RegularBall;
				mMain.OnCollectedRegularBall(rb.GetColor());
				break;
			case BallType.PowerUp:
				SpecialBall sb = b as SpecialBall;
				mMain.OnCollectedPowerUpBall(sb.GetSpecialBallType());
				break;
			default:
			break;
			}
			b.ConsumeFade ();
		}
	}
}
