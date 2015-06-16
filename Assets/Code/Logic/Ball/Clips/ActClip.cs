using System;

namespace Game.Logic.Clip
{
	public abstract class ActClip
	{
		protected Action maCallBack = null;
		protected float  mfTimeCounter = 0f;
		protected float	 mfDuring = 0f;
		protected bool   mbPlay = false;
		public ActClip ()
		{
		}

		public ActClip (float during, Action cb)
		{
			mfDuring = during;
			maCallBack = cb;
		}

		virtual public void Play(UnityEngine.Transform trans, Action cb)
		{
			mbPlay = true;
		}

		virtual public void Play(params object[] data)
		{
		}

		virtual public void Play ()
		{
			mbPlay = true;
		}

		virtual public void Pause (bool bPause)
		{
		}

		virtual public void Speed (float speed)
		{
		}

		virtual public void Tick (float dt)
		{
			if (mbPlay) {
				if (mfTimeCounter < mfDuring) {
					mfTimeCounter += dt;
                    Process(mfTimeCounter);
                }
				else
				{
					End ();
				}
			}
		}

		virtual public void Process(float curTime)
		{
			//throw new NotImplementedException ();
		}

		virtual public void End ()
		{
			//reset the pos to right position
			mbPlay = false;
			if (maCallBack != null) {
				maCallBack ();
			}
		}

		virtual public void Reset ()
		{
		}

		//internal functions
		protected float PlayPercent{ get{ return mfTimeCounter / mfDuring; } }
	}
}

