using UnityEngine;
using System.Collections;

public class PushRawProgress : MonoBehaviour
{
	[SerializeField]
	private TopGlyph[] mGlyphs = new TopGlyph[Configs.RAW_PUSH_PROGRESS_MAX];
	private int mCount = 0;
	private int mFullLength = 0;
	private PushProgressState mState = PushProgressState.Wait;
	public int AddOn (LevelConfig currentLevelConfig)
	{
		int ret = 0;
		mCount += currentLevelConfig.mPushProgress;
		ret = Mathf.FloorToInt(mCount / Configs.RAW_PUSH_PROGRESS_MAX );
		mCount = mCount % Configs.RAW_PUSH_PROGRESS_MAX;
		mFullLength += ret;
		if (ret == 0) 
		{
			PlayProgress(mCount, currentLevelConfig.mMoveDownCount);	
		}
		return ret;
	}

	void Update()
	{
		switch (mState) {
		case PushProgressState.Wait:
			if (mFullLength > 0) {
			//do a push
				mState = PushProgressState.Animating;
				PlayFullProgress();
				mFullLength--;
			}
			break;
		case PushProgressState.Animating:
			break;
		default:
			break;
		}
	}

	void AllAnimateDone()
	{
		for (int i = 0; i < Configs.RAW_PUSH_PROGRESS_MAX; i++) {
			mGlyphs[i].gameObject.SetActive(false);
        }
        mState = PushProgressState.Wait;
	}

	void PartAnimateDone()
	{
		mState = PushProgressState.Wait;
    }
    
    void PlayProgress (int originCount, int mMoveDownCount)
	{
		mState = PushProgressState.Animating;
		for (int i = 0; i < originCount; i++) {
			mGlyphs[i].gameObject.SetActive(true);
        }
		Invoke ("PartAnimateDone",0.6f);
	}

	void PlayFullProgress ()
	{
		mState = PushProgressState.Animating;
		for (int i = 0; i < Configs.RAW_PUSH_PROGRESS_MAX; i++) {
			mGlyphs[i].gameObject.SetActive(true);
        }
        Invoke ("AllAnimateDone",0.6f);
	}
}

public enum PushProgressState
{
	Wait,
	Animating,
}