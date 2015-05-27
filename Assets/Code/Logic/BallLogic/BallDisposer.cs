using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BallDisposer : MonoBehaviour 
{
	[SerializeField]
	private Transform BallLayer = null;
	[SerializeField]
	private float mSpeed = 1f;
	[SerializeField]
	private RectTransform mGameRect = null;
	[SerializeField]
	private BallGrid mGrid = null;
	[SerializeField]
	private PushRawProgress mPushProgress = null;
	[SerializeField]
	private float mFadeDelayDelta = 0.1f;
	[SerializeField]
	private float mDropDelayDelta = 0.15f;

	private DisposerState mState = DisposerState.Idle;
	private bool mbOffsetRow = false;
	private Stack<BallRow> mReverseRows = new Stack<BallRow>();
	private int mNeedToPush = 0;
	private float mTargetDistance = 0f;
	private bool mbBonusRound = false;// change this to enter bonus round
	private bool HasPowerUps{ get{ return !mbBonusRound; } }
	private bool mbRevers = false;
	private LevelConfig mLevelConfig = null;

	private struct IdxNPos
	{
		public IdxNPos(int r, int i, float x, float y)
		{
			row = r;
			index = i;
			this.x = x;
			this.y = y;
		}
		public int row;
		public int index;
		public float x;
		public float y;
	}

	void Awake()
	{
		mGrid = new BallGrid ();
	}

	void Update()
	{
		switch (mState) {
		case DisposerState.ReadyToPush:
			ReadyToDisposeBalls();
			break;
		case DisposerState.Pusing:
			DisposingBalls();
			break;
		case DisposerState.Done:
			DisposeDone();
			break;
		default:
			//mState = DisposerState.Idle;
			break;
		}
	}
	
	void ReadyToDisposeBalls ()
	{
		mState = DisposerState.Pusing;
		mTargetDistance = Configs.BALL_SIZE.y;
	}

	void DisposingBalls ()
	{
		if (mGrid != null) {
			if (mTargetDistance > 0) 
			{
				float moveDistance = Time.deltaTime * mSpeed;
				
				if (mTargetDistance < moveDistance) 
				{
					if (mbRevers) {
						mGrid.AdvanceRows(mTargetDistance);
					}
					else
					{
						mGrid.AdvanceRows(-mTargetDistance);
					}

					mTargetDistance = 0;
				}
				else
				{
					mTargetDistance -= moveDistance;
					if (mbRevers) {
						mGrid.AdvanceRows(moveDistance);
					}
					else{
						mGrid.AdvanceRows(-moveDistance);
					}
				}
				
			}
			else
			{
				mTargetDistance = 0;
				mState = DisposerState.Done;
			}
		}
	}

	void DisposeDone ()
	{
		if (mNeedToPush > 0) {
			mNeedToPush--;
		}
		else if (mNeedToPush < 0) {
			mNeedToPush++;
		}

		if (mNeedToPush !=  0) {
			mState = DisposerState.ReadyToPush;
		} else {
			mState = DisposerState.Idle;
		}

		if (mbRevers) {
			mbRevers = false;
			ReverseDone();
		}

		if (mNeedToPush < 0) {
			mbRevers = true;
		}
	}

	public IEnumerator ResetGrid (LevelConfig config)
	{
		mLevelConfig = config;
		int rowWidth = Random.Range (config.mWidth[0], config.mWidth[1] + 1);
		int mBallInitFirst = (Configs.ROW_MAX_BALLS - rowWidth) / 2;
		int mBallInitLast  =  mBallInitFirst + rowWidth;
		float mScreenRefixY = BallLayer.GetComponent<RectTransform> ().rect.height;
		mGrid.DestroyAllBalls();

		float y = mScreenRefixY / 2 + (Configs.ROW_INIT_COUNT * Configs.BALL_SIZE.y);
		float x = -(Configs.ROW_MAX_BALLS * Configs.BALL_SIZE.x) / 2 + Configs.BALL_SIZE.x - Configs.BALL_SIZE.x / 2f;
		mGrid = new BallGrid ();
		for (int i = 0; i < Configs.ROW_NUM; i++) {
			mGrid.AdvanceRows(-Configs.BALL_SIZE.y);
			BallRow row = BallRow.Create(mbOffsetRow);
			row.name += i.ToString();
			row.transform.SetParent(BallLayer);
			row.transform.localScale = Vector3.one;
			row.transform.localPosition = new Vector3(x, y, 0);
			mGrid.insert(row);
			mbOffsetRow = !mbOffsetRow;
		}

		for (int i = Configs.ROW_INIT_COUNT - 1; i >= 0; i--) {
			mGrid.FillRow(config, i, false, mBallInitFirst, mBallInitLast, HasPowerUps);
			yield return null;
		}
	}

	public BallRow BuildNextRow(LevelConfig conf)
	{
		if (mReverseRows.Count > 0) {
			return mReverseRows.Pop();
		}
		BallRow row = BallRow.Create (mbOffsetRow);
		mbOffsetRow = !mbOffsetRow;
		int rowWidth = Random.Range (conf.mWidth[0], conf.mWidth[1] + 1);
		
		int mBallInitFirst = (Configs.ROW_MAX_BALLS - rowWidth) / 2;
		int mBallInitLast  =  mBallInitFirst + rowWidth;

		row.FillRow (mGrid.GetTopRow(), conf, false, mBallInitFirst, mBallInitLast, HasPowerUps);
		return row;
	}

	public IEnumerator StartMoveDown (LevelConfig conf)
	{
		mNeedToPush = 1;
		mTargetDistance = (conf.mMoveDownCount + 1) * Configs.BALL_SIZE.y + Configs.BALL_HALF_SIZE.y + mGameRect.rect.height / 2 + mGameRect.rect.y;
		mState = DisposerState.Pusing;

		while (mState == DisposerState.Pusing || mState == DisposerState.ReadyToPush) {
			yield return null;
		}
		yield break;
	}

	public void SetShootedBall (GameObject shooted, GameObject stable)
	{
		Ball shootedBall 	= shooted.GetComponent<Ball> ();
		Ball hitedBall 		= stable.GetComponent<Ball> ();
		List<Vector2> mPossiblePos = new List<Vector2> ();
		List<IdxNPos> mPosibleSlots = new List<IdxNPos> ();
		Vector3 hitBallPos = BallLayer.InverseTransformPoint (stable.transform.position);
		Vector3 shootedPos = BallLayer.InverseTransformPoint (shooted.transform.position);
		Vector2 shootedPos2D = new Vector2 (shootedPos.x, shootedPos.y);
		BallRow curRow = hitedBall.GetBallRow ();
		int curRNum = curRow.GetNumber ();
		int hitBIdx = hitedBall.GetIndex ();
		bool bOffset = curRow.IsOffset ();
		// get positions 
		// upper row
		BallRow upRow = mGrid.GetRow (curRNum - 1);
		if (upRow != null) {
//			Debug.Log("UprowIndex:" + upRow.GetNumber());
			if (bOffset) {
				if (upRow.GetBall(hitBIdx) == null) {
					mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				}
				if (upRow.GetBall(hitBIdx + 1) == null && (hitBIdx + 1 < Configs.ROW_MAX_BALLS - 1)) {
					mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx + 1, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				}
			}
			else
			{
				if (upRow.GetBall(hitBIdx) == null) {
					mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				}
				if (upRow.GetBall(hitBIdx - 1) == null && (hitBIdx - 1 >= 0)) {
					mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx - 1, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				}
			}

		} else {
			if (bOffset) {
				mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				if (hitBIdx + 1 < Configs.ROW_MAX_BALLS - 1) {
					mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx + 1, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				}
			}
			else
			{
				mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				if (hitBIdx - 1 >= 0) {
					mPosibleSlots.Add(new IdxNPos(curRNum - 1, hitBIdx - 1, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y + Configs.BALL_SIZE.y));
				}
			}
		}
		// hit row
		if (curRow != null) {
			if (curRow.GetBall(hitBIdx - 1) == null && (hitBIdx - 1 >= 0)) {
				mPosibleSlots.Add(new IdxNPos(curRNum, hitBIdx - 1, hitBallPos.x - Configs.BALL_SIZE.x, hitBallPos.y));
			}
			if (curRow.GetBall(hitBIdx + 1) == null && (hitBIdx + 1 < Configs.ROW_MAX_BALLS - 1)) {
				mPosibleSlots.Add(new IdxNPos(curRNum, hitBIdx + 1, hitBallPos.x + Configs.BALL_SIZE.x, hitBallPos.y));
			}
		}
		// next row
		BallRow nextRow = mGrid.GetRow (curRNum + 1);
		if (nextRow != null) {
//			Debug.Log("NextRowIndex:" + nextRow.GetNumber());
			if (bOffset) {
				if (nextRow.GetBall(hitBIdx) == null) {
					mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				}
				if (nextRow.GetBall(hitBIdx + 1) == null && (hitBIdx + 1 < Configs.ROW_MAX_BALLS - 1)) {
					mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx + 1, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				}
			}
			else
			{
				if (nextRow.GetBall(hitBIdx) == null) {
					mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				}
				if (nextRow.GetBall(hitBIdx - 1) == null && (hitBIdx - 1 >= 0)) {
					mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx - 1, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				}
			}
		} else {
			if (bOffset) {
				mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				if (hitBIdx + 1 < Configs.ROW_MAX_BALLS - 1) {
					mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx + 1, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				}
			}
			else
			{
				mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx, hitBallPos.x + Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				if (hitBIdx - 1 >= 0) {
					mPosibleSlots.Add(new IdxNPos(curRNum + 1, hitBIdx - 1, hitBallPos.x - Configs.BALL_HALF_SIZE.x, hitBallPos.y - Configs.BALL_SIZE.y));
				}
			}
		}
		// find the nearest slot
		float distance = float.MaxValue;
		Vector2 testPos = new Vector2 ();
		float testDistance = 0f;
		IdxNPos nicePos = default(IdxNPos);
		foreach (var item in mPosibleSlots) {
			testPos.x = item.x;
			testPos.y = item.y;
			testDistance = Vector2.Distance(shootedPos2D, testPos);
			if (distance > testDistance) {
				distance = testDistance;
				nicePos = item;
			}
		}
		BallRow row = mGrid.GetRow (nicePos.row);
		if (row == null) {
			BallRow bottomRow = mGrid.GetBottomRow();
			float x = bottomRow.transform.localPosition.x;
			float y = bottomRow.transform.localPosition.y - Configs.BALL_SIZE.y;
			row = BallRow.Create(!bottomRow.IsOffset());
			row.transform.SetParent(BallLayer);
			row.transform.localScale = Vector3.one;
			row.transform.localPosition = new Vector3(x, y, 0);
			mGrid.push(row);
		}

		//reset parent position velocity rotation, disable collider
		Collider2D cd = shooted.GetComponent<Collider2D> ();
		cd.isTrigger = false;
		Rigidbody2D rb = shooted.GetComponent<Rigidbody2D> ();
		rb.velocity = Vector2.zero;
		rb.isKinematic = true;
		rb.rotation = 0f;

		RegularBall b = shooted.GetComponent<RegularBall> ();
		shooted.layer = Layers.StaticBall;
		b.SetBallRow (row, nicePos.index);
		row.SetBall (b, nicePos.index);
	}

	public void RemoveMatchingBalls (Ball ball, LevelConfig lvConf)
	{

		Ball[] balls = mGrid.GetMatchingBalls (ball);
		float fadeDelay = 0;
		if (balls.Length >= lvConf.mMatchingBallCount) {
			foreach (var b in balls) {
				b.DoFadeOutAndBoom(fadeDelay);
				fadeDelay += mFadeDelayDelta;
			}
		}
	}

	public void RemoveLooseBalls ()
	{
		Ball[] balls = mGrid.GetLooseBalls ();
		float delay = 0;
		foreach (var b in balls) {
			b.Fall(delay);
			delay += mDropDelayDelta;
		}
	}

	public void RemoveTargetBalls (Ball[] balls)
	{

	}

	public void AddPushProgress (LevelConfig currentLevelConfig, BallType ballType)
	{
		if (ballType == BallType.PowerUp) {
			return;
		}
		int ret = mPushProgress.AddOn (currentLevelConfig);
		if (ret == 0) {
			return;
		}
		StartCoroutine (CreateRowAsync(ret, currentLevelConfig));
	}

	IEnumerator CreateRowAsync (int ret, LevelConfig conf)
	{
		mNeedToPush += ret;
		for (int i = 0; i < ret; i++) {
			BallRow row = BuildNextRow(conf);
			BallRow topRow = mGrid.GetTopRow();
			float x = topRow.transform.localPosition.x;
			float y = topRow.transform.localPosition.y + Configs.BALL_SIZE.y;
			row.transform.SetParent(BallLayer);
			row.transform.localScale = Vector3.one;
			row.transform.localPosition = new Vector3(x, y, 0);

			mGrid.insert(row);
			yield return null;
		}
		yield return new WaitForSeconds(0.5f);
		mState = DisposerState.ReadyToPush;
		yield break;
	}

	public IEnumerator RoundFailed ()
	{
		StartCoroutine (mGrid.DisposeAll ());
		yield break;
	}

	public Ball[] GetNeighbourBalls (Ball hitted, int radius)
	{
		List<Ball> mRets = new List<Ball> ();
		if (hitted == null) {
			return null;
		}
		BallRow row = hitted.GetBallRow ();
		if (row == null) {
			return null;
		}
		bool currentOffset = row.IsOffset ();
		// find other balls here
		for (int i = 1; i <= radius; i++) 
		{
			bool bOffset = ((i % 2) > 0) && currentOffset;
			BallRow upRow = mGrid.GetRow(row.GetNumber() - i);
			GetNeighbourBallsIncreaseBallList(hitted.GetIndex(), radius, upRow, bOffset, mRets);
			BallRow downRow = mGrid.GetRow(row.GetNumber() + i); 
			GetNeighbourBallsIncreaseBallList(hitted.GetIndex(), radius, downRow, bOffset, mRets);
		}
		// find my balls here
		for (int j = 1; j <= radius; j++) {
			Ball preB = row.GetBall(hitted.GetIndex() - j);
			if (preB != null) {
				mRets.Add(preB);
			}
			Ball nxtB = row.GetBall(hitted.GetIndex() + j);
			if (nxtB != null) {
				mRets.Add(nxtB);
			}
		}
		return mRets.ToArray ();
	}

	void GetNeighbourBallsIncreaseBallList (int hittedIdx, int radius, BallRow row, bool mbOffsetRow, List<Ball> mList)
	{
		if (row == null) {
			return;
		}
		int idxl = 0;
		int idxr = 0;
		for (int i = 1; i <= radius; i++) 
		{
			if (mbOffsetRow) 
			{
				idxl = hittedIdx;
			}
			else
			{
				idxl = hittedIdx - i;
			}
			idxr = idxl + i;
			Ball upPreB = row.GetBall(idxl);
			Ball upNxtB = row.GetBall(idxr);
			if (upPreB != null) {
				mList.Add(upPreB);
			}
			if (upNxtB != null) {
				mList.Add(upNxtB);
			}
		}
	}

	public void DropLastLineByOrder()
	{
		StartCoroutine (mGrid.GetBottomRow ().FallAllByOrder ());
	}

	public void RollBack (int i)
	{
		mNeedToPush--;
		mState = DisposerState.ReadyToPush;
		mbRevers = true;
	}

	void ReverseDone ()
	{
		BallRow tr = mGrid.GetTopRow();
		mGrid.Remove(tr.GetNumber());
		mReverseRows.Push(tr);
	}

	public int GetRandBallColor ()
	{
		return Random.Range(0, mLevelConfig.mColors.Length);
	}

}

public enum DisposerState
{
	Idle,
	ReadyToPush,
	Pusing,
	Done,
}