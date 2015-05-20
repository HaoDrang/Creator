using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class BallRow : MonoBehaviour
{
	private Ball[]	mBalls = new Ball[Configs.ROW_MAX_BALLS];
	private int 	miNumber = 0;
	private bool 	mbIsOffset = false;
	private int 	miOffset = 0;
	private const int ReTryCount = 50;
	private const string PrefabName = "BallRow";
	private int miPowerUpCount = 0;
	private bool mbFirstPowerup = true;

	public void Init (bool boffset)
	{
		mbIsOffset = boffset;
		miOffset = (int)(mbIsOffset ? Configs.BALL_HALF_SIZE.x : 0);

		for (int i = 0; i < Configs.ROW_MAX_BALLS; i++) {
			mBalls [i] = null;
		}
	}

	public bool SetBall (Ball ball, int index)
	{
		if ((index < 0) || (index >= mBalls.Length)) {
			return false;
		}

		if (mBalls [index] != null) {
			mBalls [index].Waste ();
		}

		mBalls [index] = ball;
		if (ball != null) {
			ball.transform.SetParent (transform);
			ball.transform.localScale = Vector3.one;
			ball.SetLocalPos (getBallX (index), 0);
			ball.SetBallRow (this, index);
			ball.name = ball.name + index;
		}

		return true;
	}

	public void UnlinkBall (int index)
	{
		if ((index < 0) || (index >= mBalls.Length)) {
			return;
		}
		mBalls [index] = null;
	}

	public Ball GetBall (int index)
	{
		if (index < 0 || index >= mBalls.Length) {
			return null;
		}
		return mBalls [index];
	}

	int getBallX (int index)
	{
		return miOffset + (int)(Configs.BALL_SIZE.x * index);
	}

	public void SetNumber (int i)
	{
		miNumber = i;
		name = PrefabName + i.ToString ();
	}

	public int getNearestIndex (Vector3 worldPos)
	{
		Vector3 localPos = transform.InverseTransformPoint (worldPos);
		float i = ((localPos.x - (float)miOffset) / Configs.BALL_SIZE.x);
		i = Mathf.Round (i);
		return ((i >= 0) && (i < mBalls.Length)) ? (int)i : -1;
		;
	}

	public int GetBallCount ()
	{
		int ret = 0;
		for (int i = 0; i < mBalls.Length; i++) {
			if (mBalls [i] != null) {
				ret++;
			}
		}
		return ret;
	}

	public void Clear ()
	{
		Array.Clear (mBalls, 0, mBalls.Length);
	}

	public bool IsOffset ()
	{
		return mbIsOffset;
	}

	public Ball[] Balls {
		get {
			return mBalls;
		}
	}

	public void Move (float xDelta, float yDelta)
	{
		Vector3 pos = transform.localPosition;
		pos.x += xDelta;
		pos.y += yDelta;
		transform.localPosition = pos;
	}

	public int GetNumber ()
	{
		return miNumber;
	}

	public void DestroyAllBalls ()
	{
		for (int i = 0; i < mBalls.Length; i++) {
			Ball ball = mBalls [i];
			if (ball != null) {
				Destroy (ball.gameObject);
			}
		}
	}

	public void FillRow (BallRow preRow, LevelConfig conf, bool bUseDummies, int initFrom, int initTo, bool hasPowerUps)
	{
		FillRowFandomly (preRow, conf, bUseDummies, initFrom, initTo, hasPowerUps);
	}

	void FillRowFandomly (BallRow preRow, LevelConfig conf, bool bUseDummies, int initFrom, int initTo, bool hasPowerUps)
	{
		int emptyChance = conf.mEmptyOdds;
		//create balls by chance
		int slotNum = initTo - initFrom;
		int ballNum = slotNum * (100 - emptyChance) / 100;

		Ball[] balls = new Ball[ballNum];
		for (int i = 0; i < ballNum; i++) {
			balls [i] = MakeGridBall (conf, bUseDummies, 0, true, hasPowerUps);
		}

		//set them to correct positions
		if (preRow == null) { //if this row is the first row 
			int[] ballIndexList = new int[slotNum];
			for (int i = 0; i < ballIndexList.Length; i++) {
				ballIndexList [i] = i + initFrom;
			}

			ArrayTool.Shuffle<int> (ballIndexList);

			for (int i = 0; i < ballNum; i++) {
				SetBall (balls [i], ballIndexList [i]);
			}
		} else {
			// create target number of ball
			// make target slots
			bool[] targetSlots = new bool[initTo - initFrom];
			// put them into target slots
			List<int> mTarIdx = new List<int> ();
			List<int> mScdIdx = new List<int> ();
			for (int i = 0; i < Configs.ROW_MAX_BALLS; i++) {
				// go through the pre row
				Ball ball = preRow.GetBall (i);
				if (ball != null) {
					int slotIdx = i - initFrom;//(0---targetSlots.Length)
					if ((i >= initFrom) && (i < initTo)) {
						targetSlots [slotIdx] = !targetSlots [slotIdx];
					}
					if (preRow.IsOffset ()) {
						if ((i + 1) >= initFrom && (i + 1 < initTo)) {
							if (slotIdx + 1 < targetSlots.Length) {
								targetSlots [slotIdx + 1] = !targetSlots [slotIdx + 1];
							}
						}
					} else {
						if ((i - 1) >= initFrom && (i - 1 < initTo)) {
							if (slotIdx - 1 >= 0) {
								targetSlots [slotIdx - 1] = !targetSlots [slotIdx - 1];
							}
						}
					}
				}
			}

			for (int k = 0; k < targetSlots.Length; k++) {
				if (targetSlots [k]) {
					mTarIdx.Add (k + initFrom);
				} else {
					mScdIdx.Add (k + initFrom);
				}
			}

			// wash both of the lists
			ArrayTool.Shuffle<int> (mTarIdx);
			ArrayTool.Shuffle<int> (mScdIdx);
			mTarIdx.AddRange (mScdIdx);
			
			for (int j = 0; j < mTarIdx.Count; j++) {
				if (j >= ballNum) {
					break;
				}
				SetBall (balls [j], mTarIdx [j]);
			}
		}
	}
    
	void SetConnectedSlots (int i, bool[] connectedBallSlots)
	{
		if (i > 0) {
			connectedBallSlots [i - 1] = false;
		}
		if (i < connectedBallSlots.Length - 1) {
			connectedBallSlots [i + 1] = false;
		}
	}

	int CalculateConnectedSlots (BallRow ballRow, BallRow preRow, bool[] connectedBallSlots, int iFrom, int iTo)
	{
		if (preRow == null) {
			return 0;
		}

		int ret = 0;
		int index = 0;
		if (preRow.IsOffset ()) {
			for (int i = 0; i < connectedBallSlots.Length; i++) {
				index = i + iFrom;
				if ((preRow.GetBall (index) != null) || (preRow.GetBall (index - 1) != null)) {
					connectedBallSlots [i] = true;	
					ret++;
				}
			}
		} else {
			for (int i = 0; i < connectedBallSlots.Length; i++) {
				index = i + iFrom;
				if ((preRow.GetBall (index) != null) || (preRow.GetBall (index + 1) != null)) {
					connectedBallSlots [i] = true;
					ret++;
				}
			}
		}

		return ret;
	}

	Ball MakeGridBall (LevelConfig conf, bool bUseDummies, int emptyChance = 0, bool bStatic = false, bool hasPowerUps = false)
	{
		if (bUseDummies) {
			return RegularBall.Create (this, 0);
		}

		int emptyOdds = UnityEngine.Random.Range (0, 100);
		if (emptyOdds < emptyChance) {
			return null;
		}

		if (hasPowerUps) {
			if (miPowerUpCount == 0) {
				var delay = mbFirstPowerup ? conf.mPowerUpDelayInitial : conf.mPowerUpDelay;
				miPowerUpCount = UnityEngine.Random.Range (delay [0], delay [1] + 1);
				mbFirstPowerup = false;
			}

			if (--miPowerUpCount <= 0) {
				return MakePowerUp (conf.eTypes, bStatic);
			}
		}

		return MakeRegularBall (conf.Colors.Length, bStatic);
	}

	Ball MakePowerUp (SpecialBallType[] eTypes, bool bStatic = false)
	{
		int n = UnityEngine.Random.Range (0, eTypes.Length);
		Ball b = SpecialBall.Create (this, eTypes [n]);
		b.GetComponent<Rigidbody2D> ().isKinematic = bStatic;
		return b;
	}

	Ball MakeRegularBall (int colorsCount, bool bStatic = false)
	{
		int n = UnityEngine.Random.Range (0, colorsCount);
		Ball b = RegularBall.Create (this, n);
		b.GetComponent<Rigidbody2D> ().isKinematic = bStatic;
		return b;
	}

	public static BallRow Create (bool mbOffsetRow)
	{
		GameObject bow = PrefabMgr.Instance.CreateCopy (PrefabName);
		bow.name = PrefabName;
		BallRow br = bow.GetComponent<BallRow> ();
		br.Init (mbOffsetRow);
		return br;
	}

	public void DisposeAll ()
	{
		foreach (var ball in Balls) {
			if (ball != null) {
				ball.Dispose ();
			}
		}
	}

	public IEnumerator FallAllByOrder()
	{
		for (int i = 0; i < mBalls.Length; i++) {
			Ball b = mBalls[i];
			if (b != null) {
				b.Fall();
				yield return new WaitForSeconds (0.1f);
			}
		}

		yield break;
	}
}
