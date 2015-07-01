
using System;

namespace Game.Logic.Clip
{
	public class RowLongMoveClip : RowMoveClip
	{
		public RowLongMoveClip(UnityEngine.Transform trans, Action cb) : base(trans, cb)
		{

		}

		public void LongMove(float l)
		{
			if (l <= 0) {
				return;
			}
			float f1 = mfOffset * mcCurve.keys [1].value;
			float f2 = mfOffset * (mcCurve.keys [mcCurve.keys.Length - 3].value);
			float f3 = mfOffset * (mcCurve.keys [mcCurve.keys.Length - 2].value);

			float t1 = mfDuring * mcCurve.keys [1].time;
			float t2 = mfDuring * mcCurve.keys [mcCurve.keys.Length - 3].time;
			float t3 = mfDuring * mcCurve.keys [mcCurve.keys.Length - 2].time;

			float s = f2 - f1;
			float b = l + s - mfOffset;
			float mul = b / s;

			float ds = t2 - t1;
			float db = ds * mul;
			float during = db + t1 + (mfDuring - t2);

			UnityEngine.Keyframe[] keys = new UnityEngine.Keyframe[mcCurve.keys.Length];
			keys [0] = new UnityEngine.Keyframe (0,0);

			keys [4] = new UnityEngine.Keyframe (1,1);

			keys [1] = new UnityEngine.Keyframe (t1 / l, f1 / l);
			keys [2] = new UnityEngine.Keyframe (1f - (mfDuring - t2) / during, (f2 - mfOffset) / l + 1f);
			keys [3] = new UnityEngine.Keyframe (1f - (mfDuring - t3) / during, 1f - (mfOffset - f3) / l);

			mcCurve = new UnityEngine.AnimationCurve (keys);

			mfOffset = l;
			mfDuring = during;
		}
	}
}

