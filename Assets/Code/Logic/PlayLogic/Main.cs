/*
 * Main Logic Of the Game
 */
using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour, IGameOverEventHandler
{
	[SerializeField]
	private BallDisposer	mBallListController = null;
	[SerializeField]
	private BallShooter 	mShooter = null;

	private LevelConfig[]	mLevelConfig;
	private LevelConfig 	mCurrentLevelConfig = null;
	[SerializeField]
	private Rect			mGameRect;
	[SerializeField]
	private Camera			mMainCamera = null;
	[SerializeField]
	private LevelEnum		meCurLevel = LevelEnum.Beginner;
	[SerializeField]
	private UILogic			meUILogic = null;
	
	private bool mWin = false;
	//main game state
	private GameState mState = GameState.FIRST_OPEN;

	//-----Gets begin
	private LevelConfig CurrentLevelConfig
	{
		get
		{
			return mCurrentLevelConfig;
		}
	}
	//-----Gets End
	
	private IEnumerator ResetGrid()
	{
		RegularBall.ColorArray = CurrentLevelConfig.mColors;
		yield return StartCoroutine (mBallListController.ResetGrid (CurrentLevelConfig));
		yield return StartCoroutine(mBallListController.StartMoveDown(CurrentLevelConfig));
		mState = GameState.ROUND_START;
		yield break;
	}

	void Start()
	{
		// abouve all
		mLevelConfig = LevelConfig.LoadLevelConfigs (Property.Instante);

	}

	void Update()
	{
		StateProcessor ();
	}

	void StateProcessor ()
	{
		switch (mState) {
		case GameState.FIRST_OPEN:
			// Do something
			mState = GameState.INTRODUCTION;
			break;
		case GameState.INTRODUCTIONDONE:
			mState = GameState.PREPARE;
			StartCoroutine(ResetGrid());
			break;
		case GameState.PREPARE:
			break;
		case GameState.ROUND_START:
			//add something need to be done when start
			//RoundStart();
			mState = GameState.ROUND_PLAYING;
			break;
		case GameState.ROUND_PLAYING:
			if (Input.GetKeyUp( KeyCode.Mouse0)) 
			{
				BallType bt = BallType.Regular;
				if (mShooter.Shoot(ref bt)) {
					mBallListController.AddPushProgress(CurrentLevelConfig, bt);
				}
			}
			mShooter.Rotate(Input.mousePosition);
			if (Input.GetKeyDown(KeyCode.F1))
			{
				mBallListController.DropLastLineByOrder();
			}
			// 
			if (Input.GetKeyDown(KeyCode.Escape)) {
				meUILogic.ShowConfig();
			}
			break;
		case GameState.ROUND_OVER:
			break;
		default:
			break;
		}


	}

	bool ColliderCallBack (GameObject obj, GameObject target)
	{
		bool ret = false;
		switch (target.tag) {
		case Tags.TOP_BORDER:
		case Tags.LEFT_BORDER:
		case Tags.RIGHT_BORDER:
		case Tags.BOTTOM_BORDER:
		case Tags.TUNNEL_BORDER:
			ret =  ColliderBorder(obj, target);;
			break;
		case Tags.BALL:
			CollideOtherBall(obj, target);
			ret =  true;
			CheckSameColorBall(obj.GetComponent<Ball>());
			CheckLooseBalls();
            break;
            default:
			ret =  false;
            break;
        }

		return ret;
	}

	void CollideOtherBall (GameObject shooted, GameObject stable)
	{
		Ball b = shooted.GetComponent<Ball> ();
		switch (b.GetBallType()) {
		case BallType.Regular:
			mBallListController.SetShootedBall (shooted, stable);
			break;
		case BallType.PowerUp:
			SpecialBall sb = b as SpecialBall;
			sb.HandleCollidOtherBall(mBallListController, shooted, stable);
			break;
		default:
			break;
		}
	}

	bool ColliderBorder (GameObject obj, GameObject border)
	{
		bool ret = false;
		Ball b = obj.GetComponent<Ball> ();
		switch (b.GetBallType()) {
		case BallType.Regular:
			break;
		case BallType.PowerUp:
			SpecialBall sb = b as SpecialBall;
			ret = sb.HandleCollidBorder(obj, border);
			break;
		default:
			break;
		}

		return ret;
	}

	void CheckSameColorBall (Ball ball)
	{
		switch (ball.GetBallType()) {
		case BallType.Regular:
			int removeRet = mBallListController.RemoveMatchingBalls (ball, CurrentLevelConfig);
			if (removeRet > 0) {
				meUILogic.CreateConsumeText(removeRet, 
				                            ball.CurrentColor, 
				                            ball.transform.position);
			}
			break;
		case BallType.PowerUp:
		default:
			break;
		}
	}

	void CheckLooseBalls ()
	{
		int ret = mBallListController.RemoveLooseBalls ();
		if (ret > 0) {
			meUILogic.CreateDropText(ret, Vector3.zero);
		}
	}

	public void BallOutOfBoard()
	{
		mState = GameState.ROUND_OVER;
		StartCoroutine (GameOverFlow());
	}

	IEnumerator GameOverFlow()
	{
		mWin = false;
		yield return StartCoroutine (mBallListController.RoundFailed());// do ball lossen

		meUILogic.ShowRollUp (mWin);

		yield break;
	}

	public void GameBegin()
	{
		mState = GameState.INTRODUCTIONDONE;
		if (mShooter != null) {
			mShooter.Init(CurrentLevelConfig, mMainCamera, ColliderCallBack, AchievementDetector);
		}
	}

	public void ReplayRound()
	{
		mState = GameState.PREPARE;
		StartCoroutine(ResetGrid());
	}

	public void GameWin()
	{
		//Play win effects
		//Show End RollUp
	}

	//--------------------------------------------------
	// Collect events
	//--------------------------------------------------
	private int miScore = 0;
	public void OnCollectedRegularBall (int iColor)
	{
		meUILogic.UpdateScore (++miScore);
	}

	public void OnCollectedPowerUpBall (SpecialBallType specialBallType)
	{
		StartCoroutine(SetPowerUpToShooter(BallType.PowerUp, (int)specialBallType));
	}

	IEnumerator SetPowerUpToShooter(BallType eBallType, int spBallType)
	{
		GameObject shooter = GameObject.Find (BallDefines.SHOOTER_OBJECT_NAME);
		shooter.GetComponent<BallShooter> ().PrepareSmall (eBallType, (int)spBallType);
		yield break;
	}

	public void SetDifficult (LevelEnum lv)
	{
		mCurrentLevelConfig = mLevelConfig [(int)lv].CreateNewConfig (ColorBoard.GetColorArray(1));
	}

	//----------------------------------------------------
	// Event System AchievementDetector
	//----------------------------------------------------
	public AchievementDetection AchievementDetector {
		get;
		set;
	}
}