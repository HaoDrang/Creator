using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BallShooter : MonoBehaviour {
	[SerializeField]
	private float shootForce = 20f;
	[SerializeField]
	private ShockBackMovent mShockMove = null;
	[SerializeField]
	private BigBallSlot mBigBallSlot = null;
	[SerializeField]
	private SmallBallSlot mSmallBallSlot = null;
	[SerializeField]
	private Transform mRotateRoot = null;
	[SerializeField]
	private AimLine mAimLine = null;
	private Camera mCam = null;
	private LevelConfig	mLevelConfig = null;
	private Vector2		mShootScreenPos = new Vector2();
	private RectTransform mParentTrans = null;
	private BoolDelegate_GObj_GObj mCollideCallBack = null;
	private const string RegularPrefabName			= "RegularBall";
	private const string SpecialPrefabName			= "SpecialBall";
	private bool mbPreventShooter = false;
	private AchievementDetection mDetector = null;

	void Start()
	{
		gameObject.name = BallDefines.SHOOTER_OBJECT_NAME;
	}

	public void Init(LevelConfig lvConf, Camera cam, BoolDelegate_GObj_GObj colCb = null, AchievementDetection detector = null)
	{
		Vector3 pos = transform.position;
		Vector2 sp = RectTransformUtility.WorldToScreenPoint (cam, pos);

		mCam = cam;
		mCollideCallBack = colCb;
		mParentTrans = this.transform.parent.GetComponent<RectTransform> ();
        mLevelConfig = lvConf;

		mDetector = detector;

		mSmallBallSlot.Init (lvConf.mColors);
		mBigBallSlot.Init (lvConf.mColors);

		PrepareSmall (mLevelConfig);
		PrepareBig ();
		PrepareSmall (mLevelConfig);
    }

	public void Rotate(Vector2 mouseScreenPos)
	{
		SelfRotate (mouseScreenPos);

	}

	void SelfRotate(Vector2 mouseScreenPos)
	{		
		Vector3 pos;
		RectTransformUtility.ScreenPointToWorldPointInRectangle (mParentTrans, mouseScreenPos, mCam, out pos);
		Vector3 dir = pos - transform.position; 
		float angle = Mathf.Atan2(dir.y,dir.x) * Mathf.Rad2Deg; 
		mRotateRoot.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward); 

		mAimLine.AimTo (transform.position, angle);
	}

	public bool Shoot(ref BallType ballType)
	{
		if (mbPreventShooter) {
			return false;
		}

		// Create a new Ball
		if (mShockMove.Cooling) 
		{
			return false;
        }

		mShockMove.Shock ();
		ballType = mBigBallSlot.GetBallType();
		CreateBallAndAddForce (ballType, mBigBallSlot.BallColorInfo);

		mDetector.FireEvent (GameEvent.BallShoot, ballType, mBigBallSlot.BallColorInfo, mLevelConfig);

		PrepareBig (mSmallBallSlot.BallType, mSmallBallSlot.BallColorInfo);
		PrepareSmall (mLevelConfig);

		return true;
    }

	void CreateBallAndAddForce (BallType eBallType, int param1)
	{
		var direction = mBigBallSlot.transform.up;

		switch (eBallType) {
		case BallType.Regular:
			GameObject rbBallObj = PrefabMgr.Instance.CreateCopy (RegularPrefabName);
			RegularBall rb = rbBallObj.GetComponent<RegularBall>();
			rb.Init(null, param1);
			rb.Shoot(this.mParentTrans, mBigBallSlot.transform.position,  
			         mRotateRoot.rotation, mBigBallSlot.transform.up * shootForce, 
			         mCollideCallBack);
			break;
		case BallType.PowerUp:
			GameObject spBallObj = PrefabMgr.Instance.CreateCopy (SpecialPrefabName);
			SpecialBall sb = spBallObj.GetComponent<SpecialBall>();
			sb.Init(null, (int)BallColor.Gold, (SpecialBallType)param1);
			ShootBall(sb);
			break;
		default:
			break;
		}
	}

	void ShootBall (Ball b)
	{
		b.Shoot(this.mParentTrans, mBigBallSlot.transform.position,  
		        mRotateRoot.rotation, 
		        mBigBallSlot.transform.up * shootForce, 
		        mCollideCallBack);
	}

    public void PrepareBig()
	{
		PrepareBig (BallType.Regular, (int)BallColor.Blue);
	}

	public void PrepareBig(BallType eType, int c)
	{
		mBigBallSlot.PrepareBall (eType, c);
	}

	public void ChangeBall()
	{

	}

	public void PrepareSmall(LevelConfig lvConf)
	{
		mSmallBallSlot.PrepareRandBall (lvConf.mColors.Length);
		//mSmallBallSlot.PrepareRandPowerUpBall ();
	}


	public void PrepareSmall(int color)
	{

	}

	public void PrepareSmall (BallType mBallType, int i)
	{
		mSmallBallSlot.PrepareBall(mBallType, i);
	}

	public void SaveBall()
	{

	}

	public void ShootDone()
	{

	}


	public bool PreventShooter {
		get {
			return mbPreventShooter;
		}
		set {
			mbPreventShooter = value;
		}
	}

	public Transform RotateRoot {
		get {
			return mRotateRoot;
		}
	}
}
