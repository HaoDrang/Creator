
namespace Game.Logic
{
	public class BallCtrl_StateHandlerProvider
	{
		public BallCtrl_StateHandlerProvider()
		{

		}

		public BallCtrl_StateHandler GetHandler(BallState currentState)
		{
			BallCtrl_StateHandler handler = null;
			switch (currentState) {
			case BallState.DISPOSED:
				handler = new BallCtrl_DisposeHandler();
				break;
			case BallState.FLY:
				handler = new BallCtrl_FlyHandler();
				break;
			case BallState.MOVE:
				handler = new BallCtrl_MoveHandler();
				break;
			case BallState.PREPARE:
				handler = new BallCtrl_PrepareHandler();
				break;
			case BallState.READY:
				handler = new BallCtrl_ReadyHandler();
				break;
			case BallState.STATIC:
				handler = new BallCtrl_StaticHandler();
				break;
			default:
				break;
			}

			return handler;
		}
	}
}

