
using System;

namespace Game.Logic.Clip
{
	public class BallShakeClip : ActClip
	{
		protected float mfHorizontalOffset 	= 0f;
		protected float mfVerticalOffset 	= 0f;
		protected UnityEngine.Transform mtTransform = null;
		protected UnityEngine.Vector3 mvBasePos = default(UnityEngine.Vector3);

		protected const string BallShake_Key 		= "ballshake";
		protected const string Vertical_Offset_Key 	= "vo";
		protected const string Horizontal_Offset_Key= "ho";
		protected const string During_Key = "during";

		public BallShakeClip ()
		{
			mfHorizontalOffset 	= Property.Ins.GetFloat(BallShake_Key + "." + Horizontal_Offset_Key);
			mfVerticalOffset 	= Property.Ins.GetFloat(BallShake_Key + "." + Vertical_Offset_Key);
			mfDuring 			= Property.Ins.GetFloat (BallShake_Key + "." + During_Key);
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

		public override void Play (UnityEngine.Transform trans, Action cb)
		{
			mtTransform = trans;
			mvBasePos 	= mtTransform.localPosition;
			maCallBack	= cb;
			base.Play ();
		}

		private float CalculateValue(float percent)
		{
			return UnityEngine.Mathf.Sin (UnityEngine.Mathf.PI * 2f * percent);
		}
	}
}

