using System;
using Game.Logic.Events;
using System.Collections.Generic;

namespace Game.Logic.Clip
{
	public class RowAnimator : AlgebraAnimator
	{
		[UnityEngine.SerializeField]
		private GridPushDone mPushEvt = null;
		private int miStep = 1;
		private bool mbMoving = false;
		private ActClip mcCurrentClip = null;
		private Queue<ActClip> mcClipQ = null;
		public override void Start ()
		{
			base.Start ();
			mcClipQ = new Queue<ActClip> ();
			if (_factory == null) {
				ClipGenerator.Register<RowMoveClip> (() => new RowMoveClip (transform, moveDone));
			}
		}
		
		protected override ActClip GetCurrentClip (ClipEnum clipEnum)
		{
			if (NextMove) {
				return mcClipQ.Dequeue ();
			} else {
				return GenerateClip(clipEnum);
			}
		}

		public ActClip GenerateClip(ClipEnum clipEnum)
		{
			switch (clipEnum) {
			case ClipEnum.RowMove:
				mcCurrentClip = ClipGenerator.Generate<RowMoveClip> ();
				break;
			default:
				break;
			}
			return mcCurrentClip;
		}

		public void Move(int istep = 1)
		{
			int d = (int)UnityEngine.Mathf.Sign (istep);
			for (int i = 0; i < UnityEngine.Mathf.Abs(istep); i++) {
				RowMoveClip rmc = (RowMoveClip)GenerateClip(ClipEnum.RowMove);
				rmc.step = d;
				mcClipQ.Enqueue (rmc);
			}

			if (mbMoving) {
				return;
			}

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
			if (NextMove) {
				mbMoving = true;
				Play (ClipEnum.RowMove);
			}
		}

		private bool NextMove{ get{ return mcClipQ.Count > 0; } }
	}
}