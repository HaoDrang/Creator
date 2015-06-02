public class TimeCounter
{
	private float mDuring = 0f;
	private float mfTimeCounter = 0f;
	private VoidDelegate_F_F mTickCallBack = null;
	private VoidDelegate mCountDoneCallBack = null;
	private bool mbPlay = false;

	public TimeCounter (float during, VoidDelegate_F_F tcb, VoidDelegate mcdcb)
	{
		mDuring 			= during;
		mTickCallBack 		= tcb;
		mCountDoneCallBack 	= mcdcb;
	}

	public bool Play()
	{
		if (mbPlay) {
			return false;
		}
		mbPlay = true;
		return true;
	}

	public bool Stop()
	{
		if (!mbPlay) {
			return false;
		}
		mbPlay = false;
		return true;
	}

	public void Tick(float deltaTime)
	{
		if (mbPlay) {
			if(mfTimeCounter < mDuring)
			{
				mfTimeCounter += deltaTime;
				if (mTickCallBack != null) {
					mTickCallBack(mfTimeCounter, mDuring);
				}
			}
			else
			{
				if (mCountDoneCallBack != null) {
					mCountDoneCallBack();
				}
				mbPlay = false;
			}
		}

	}

	public void Reset()
	{
		mbPlay = false;
		mfTimeCounter = 0;
	}
}

