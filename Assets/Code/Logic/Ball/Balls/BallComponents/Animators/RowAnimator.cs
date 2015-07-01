using System;
using Game.Logic.Events;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Logic.Clip
{
	public class RowAnimator : AlgebraAnimator
	{
		public AnimationCurve ac1 = null;
		public AnimationCurve ac2 = null;
		[UnityEngine.SerializeField]
		private GridPushDone mPushEvt = null;
		private int miStep = 1;
		private bool mbMoving = false;
		private ActClip mcCurrentClip = null;
		private Queue<ActClip> mcClipQ = null;

		public void Awake()
		{
			mcClipQ = new Queue<ActClip> ();

			ClipGenerator.Register<RowMoveClip> (() => new RowMoveClip (transform, moveDone));
			ClipGenerator.Register<RowLongMoveClip> (() => new RowLongMoveClip (transform, moveDone));
		}

		public override void Start ()
		{
			base.Start ();
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
			case ClipEnum.RowLongMove:
//				MonoBehaviour.print("generator a long move clip");
				mcCurrentClip = ClipGenerator.Generate<RowLongMoveClip>();
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

		public void LongMove (float length)
		{
			if (mbMoving && mcCurrentClip != null
			    && mcCurrentClip.GetType() == typeof(RowLongMoveClip)) {
				return;
			}

			int d = (int)UnityEngine.Mathf.Sign (length);
			RowLongMoveClip rlmc = (RowLongMoveClip)GenerateClip(ClipEnum.RowLongMove);
			ac1 =  new AnimationCurve (rlmc.mcCurve.keys);
			rlmc.LongMove (length);
			ac2 = rlmc.mcCurve;
			mcClipQ.Enqueue (rlmc);

			if (mbMoving) {
				return;
			}
			
			mbMoving = true;
			Play (ClipEnum.RowLongMove);
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