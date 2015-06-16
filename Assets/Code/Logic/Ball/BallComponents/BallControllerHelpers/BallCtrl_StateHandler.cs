using System;
namespace Game.Logic
{
	public class BallCtrl_StateHandler
	{
		protected BallState mState;
		public BallCtrl_StateHandler()
		{

		}
		public BallCtrl_StateHandler(BallState state)
		{
			mState 	= state;
		}

		public BallState State{ get{return mState;} }
		virtual public void Execute(BallState formerState, UnityEngine.GameObject obj){}
	}
}

