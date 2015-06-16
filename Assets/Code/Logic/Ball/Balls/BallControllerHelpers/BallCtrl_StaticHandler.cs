
namespace Game.Logic
{
	public class BallCtrl_StaticHandler : BallCtrl_StateHandler
	{
		public BallCtrl_StaticHandler ()
		{
			mState = BallState.STATIC;
		}

		public override void Execute (BallState formerState, UnityEngine.GameObject obj)
		{
			switch (formerState) {
			case BallState.PREPARE:
				//
				UnityEngine.Debug.LogError("Ball State Changed from PREPARE to STATIC, please make sure this is proper");
				break;
			case BallState.READY:

				break;
			case BallState.FLY:
				//ball should stop fly here
				UnityEngine.Rigidbody2D r2d = obj.GetComponent<UnityEngine.Rigidbody2D>();
				if (r2d != null) {
					r2d.angularVelocity = 0;
					r2d.velocity = UnityEngine.Vector2.zero;
					r2d.isKinematic = true;
					r2d.rotation = 0f;

					obj.layer = Layers.StaticBall;

					BallController fbc = obj.GetComponent<BallController>();
					if (fbc != null) {
						if (fbc.algebraAnimation != null) {
							fbc.algebraAnimation.Play(Clip.ClipEnum.BallWave);
						}
					}
				}
				break;
			case BallState.MOVE:
				//Play a wave motion here
				BallController bc = obj.GetComponent<BallController>();
				if (bc != null) {
					if (bc.algebraAnimation != null) {
						bc.algebraAnimation.Play(Clip.ClipEnum.BallWave);
					}
				}
				break;
			case BallState.DISPOSED:
				// this shouldnt happen
				break;
			default:
				break;
			}
		}
	}
}

