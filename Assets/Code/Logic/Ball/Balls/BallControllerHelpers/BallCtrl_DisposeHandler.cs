
namespace Game.Logic
{
	public class BallCtrl_DisposeHandler : BallCtrl_StateHandler
	{
		public BallCtrl_DisposeHandler()
		{
			mState = BallState.DISPOSED;
		}

		public override void Execute (BallState formerState, UnityEngine.GameObject obj)
		{
			switch (formerState) {
			case BallState.PREPARE:
				//from prepare to dispose
				//just play a effect and destroy this
				if (obj != null) {
					//play an incredible effect here
					UnityEngine.GameObject.Destroy(obj);
				}
				break;
			case BallState.READY:
				break;
			case BallState.FLY:
				break;
			case BallState.MOVE:
				break;
			case BallState.STATIC:
				break;
			case BallState.DISPOSED:
				break;
			default:
				break;
			}
		}
	}
}

