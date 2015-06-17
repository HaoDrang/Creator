using System;
using UnityEngine;

namespace Game.Logic.Clip
{
	public class RowMoveClip : ActClip
	{
		private int miStep = 1;
		private float mfOffset = 1;
		private Transform mtTrans = null;
		private AnimationCurve mcCurve = null;
		private Vector3 mvVector = default(Vector3);
		private Property mProp = null;
		private int	 miDirection = 1;
		private const string RowMoveKey = "rowmove";
		private const string DuringKey = "during";
		private const string OffsetKey = "steplength";
		private const string CurveKey = "curve";
		private const string CurveKeyCount = "keycount";
		private const int 	 KeyValueNum = 4;

		public int step {
			get {
				return miStep;
			}
			set {
				miStep = value;
			}
		}

		public RowMoveClip (UnityEngine.Transform trans, Action cb)
		{
			maCallBack = cb;
			mtTrans = trans;
			mProp = Property.Ins;
			//read properties here
			mfDuring = mProp.GetFloat (RowMoveKey + "." + DuringKey);
			mfOffset = mProp.GetFloat (RowMoveKey + "." + OffsetKey);

			int keyNum = mProp.GetInt (RowMoveKey + "." + CurveKey + "." + CurveKeyCount);

			if (keyNum > 0) {
				mcCurve = new AnimationCurve ();
				for (int i = 0; i < keyNum; i++) {
					float[] values = mProp.GetFloatArray (RowMoveKey + "." + CurveKey + "." + i);
					if (values.Length == KeyValueNum) {
						mcCurve.AddKey (new Keyframe (values [0], values [1], values [2], values [3]));
					} else {
						Debug.LogError ("Inproper length " + RowMoveKey + "." + CurveKey + "." + i + " : length:" + values.Length);
					}
				}
			}

//			float[][] values = new float[5][4];
			// create a action curve
//			mcCurve = new AnimationCurve (new Keyframe(0, 	 0f,  	0,0),
//			                              new Keyframe(0.2f, -0.1f,  	0,0),
//			                              new Keyframe(0.8f, 1.1f,  	1.1f,0),
//			                              new Keyframe(0.9f, 0.95f,  0,0),
//			                              new Keyframe(1f, 	 1f,  	0,0));

//			Debug.Log ("~~~~~~~~~~~~" + mcCurve.Evaluate(1f));
		}

		public override void Play ()
		{
			miDirection = (int)Mathf.Sign (miStep);
			miStep = miDirection * (Mathf.Abs (miStep) - 1);
			PrepareMove ();
			base.Play ();
		}

		public override void Process (float curTime)
		{
			if (mtTrans != null) {
				MovePos (CalculateValue (PlayPercent));
			}
		}

		private void MovePos (float f)
		{
			Vector3 pos = mvVector;
			pos.y -= f;
			mtTrans.localPosition = pos;
		}

		private void SetMoveEndPos ()
		{
			MovePos (CalculateValue (1f));
		}

		public override void End ()
		{
			if (Mathf.Abs (miStep) > 0) {
				SetMoveEndPos ();
//				Debug.Log ("EndPos:" + mtTrans.localPosition);
				this.Play ();
			} else {
				base.End ();
			}
		}

		private void PrepareMove ()
		{
			mvVector = mtTrans.localPosition;
			Debug.Log ("PrepareMove:" + mvVector);
			mfTimeCounter = 0f;
		}

		private float CalculateValue (float f)
		{
			return mcCurve.Evaluate (f) * mfOffset * miDirection;
		}

		public void AddStep (int istep)
		{
			miStep += istep;
		}
	}
}

