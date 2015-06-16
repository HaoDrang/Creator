using System;
using UnityEngine;
namespace Game.Logic.Clip
{
	public class RowMoveClip : ActClip
	{
		private int miStep = 1;
		private UnityEngine.Transform mtTrans = null;
		public int step {
			get{
				return miStep;
			}
			set{
				miStep = value;
			}
		}

		public RowMoveClip (UnityEngine.Transform trans, Action cb)
		{
			maCallBack = cb;
			mtTrans = trans;
			mfDuring = 1f;
		}

		public override void Process (float curTime)
		{
			if (mtTrans != null) {
				Vector3 pos = mtTrans.localPosition;
				pos.y -= curTime * 1;
				mtTrans.localPosition = pos;
			}
		}
	}
}

