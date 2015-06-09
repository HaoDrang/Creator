using System;
namespace Game.Logic.Clip
{
	public abstract class ActClip
	{
		protected Action maCallBack = null;
		public ActClip(){}
		public ActClip(Action cb){ maCallBack = cb; }
		virtual public void Play(){}
		virtual public void Pause(bool bPause){}
		virtual public void Speed(float speed){}
		virtual public void End(){
			if (maCallBack != null) {
				maCallBack();
			}
		}
		virtual public void Reset(){}
	}
}

