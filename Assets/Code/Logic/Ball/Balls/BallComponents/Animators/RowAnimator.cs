using System;
using Game.Logic.Events;

namespace Game.Logic.Clip
{
	public class RowAnimator : AlgebraAnimator
	{
		[UnityEngine.SerializeField]
		private GridPushDone mPushEvt = null;
		private int miStep = 1;
		private bool mbMoving = false;
		public override void Start ()
		{
			base.Start ();
			if (_factory == null) {
				ClipGenerator.Register<RowMoveClip> (() => new RowMoveClip (transform, moveDone));
			}
		}
		
		protected override ActClip GetCurrentClip (ClipEnum clipEnum)
		{
			ActClip ac = null;
			switch (clipEnum) {
			case ClipEnum.RowMove:
				ac = ClipGenerator.Generate<RowMoveClip> ();
				((RowMoveClip)ac).step = miStep;
				break;
			default:
				break;
			}
			return ac;
		}

		public void Move(int istep)
		{
			if (mbMoving) {
				return;
			}
			miStep = istep;
			mbMoving = true;
			Play (ClipEnum.RowMove);
		}

		public GridPushDone PushEvent
		{
			get
			{
				return mPushEvt;
			}
		}

		private void moveDone ()
		{
			mbMoving = false;
			if (mPushEvt != null) {
				mPushEvt.Invoke();
			}
		}
	}
}