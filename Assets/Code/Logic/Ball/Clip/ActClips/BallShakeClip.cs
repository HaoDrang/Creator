
using System;

namespace Game.Logic.Clip
{
	public class BallShakeClip : ActClip
	{
		protected float mfHorizontalOffset 	= 0f;
		protected float mfVerticalOffset 	= 0f;
		protected UnityEngine.Transform mtTransform = null;
		protected UnityEngine.Vector3 mvBasePos = default(UnityEngine.Vector3);

		public BallShakeClip (float fHOffset, float fVOffset, 
		                      UnityEngine.Transform trans, 
		                      float during, Action cb) : base(during, cb)
		{
			mfHorizontalOffset 	= fHOffset;
			mfVerticalOffset 	= fVOffset;
			mtTransform = trans;
			mvBasePos = mtTransform.localPosition;
		}

		public override void Process (float curTime)
		{
			float _h_offset = CalculateValue (PlayPercent) * mfHorizontalOffset;
			float _v_offset = CalculateValue (PlayPercent) * mfVerticalOffset;

			UnityEngine.Vector3 updatePos = mvBasePos;
			updatePos.x += _h_offset;
			updatePos.y += _v_offset;

			mtTransform.localPosition = updatePos;
		}

		public override void End ()
		{
			// handle the position first
			mtTransform.localPosition = mvBasePos;
			// call the baseclass end function last
			base.End ();
		}

		private float CalculateValue(float percent)
		{
			return UnityEngine.Mathf.Sin (UnityEngine.Mathf.PI * 2f * percent);
		}
	}
}

