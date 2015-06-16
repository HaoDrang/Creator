using UnityEngine;
using System.Collections;

public class SpecialBall : Ball 
{
	const string ImgPrefix_Big 		= "B_";	//means big
	const string ImgPrefix_Small 	= "S_";	//means small
	const string PrefabName			= "SpecialBall";

	private SpecialBallType mSpecialBallType = 0;

	public void Init (BallRow row, int c, SpecialBallType ballType)
	{
		mBallType = BallType.PowerUp;
		base.Init (row);
		SetSpecialType (ballType);
	}

	public static Ball Create (BallRow ballRow, SpecialBallType ballType)
	{
		GameObject ball = PrefabMgr.Ins.CreateCopy (PrefabName);
//		ball.layer = Layers.StaticBall;
		ball.name = PrefabName + "_" + ballType.ToString();
		SpecialBall b = ball.GetComponent<SpecialBall> ();
		b.Init (ballRow, (int)BallColor.Green, ballType);

		return b;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		switch (mSpecialBallType) {
		case SpecialBallType.CHAINSAW:
			if (mTrigerCallBack != null) {
				mTrigerCallBack(gameObject, other.gameObject);
			}
			break;
		default:
			break;
		}
	}

	public void SetSpecialType(SpecialBallType eType)
	{
		mSpecialBallType = eType;
		Animator amn = GetComponent<Animator> ();
		amn.Play (ImgPrefix_Big + ((int)eType));
	}

	public override bool IsPowerUp ()
	{
		return true;
	}

	public override void Fall (float delay)
	{
		base.Fall (delay);
	}

	public override int GetBallTypeCode ()
	{
		return base.GetBallTypeCode () * BallDefines.BALL_TYPE_SPLITER + (int)mSpecialBallType;
	}

	IEnumerator SetPowerUpToShooter()
	{
		GameObject shooter = GameObject.Find (BallDefines.SHOOTER_OBJECT_NAME);
		shooter.GetComponent<BallShooter> ().PrepareSmall (mBallType, (int)mSpecialBallType);
		yield return null;
		Destroy (gameObject);
		yield break;
	}

	virtual public void HandleCollidOtherBall (BallDisposer mBallListController, GameObject shooted, GameObject stable)
	{
		PowerUpLogicBase logic = GetLogic(mSpecialBallType);
		logic.HandleCollidOtherBall (mBallListController, shooted, stable);
	}

	public override void Shoot (Transform p, Vector3 worldPos, Quaternion worldRot, 
	                            Vector2 velocity, BoolDelegate_GObj_GObj cb)
	{
		mTrigerCallBack = cb;
		PowerUpLogicBase logic = GetLogic(mSpecialBallType);
		logic.Shoot (gameObject, p, worldPos, worldRot, velocity);
	}

	public void SetSpecialBallType (SpecialBallType sbt)
	{
		mSpecialBallType = sbt;
	}

	public SpecialBallType GetSpecialBallType()
	{
		return mSpecialBallType;
	}

	virtual public bool HandleCollidBorder (GameObject shooted, GameObject border)
	{
		PowerUpLogicBase logic = GetLogic(mSpecialBallType);
		return logic.HandleColledBorder (shooted, border);
	}

	
	virtual public PowerUpLogicBase GetLogic(SpecialBallType powerUpType)
	{
		PowerUpLogicBase logic = null;
		switch (mSpecialBallType) {
		case SpecialBallType.BOMB:
			logic = new PW_Bomb();
			break;
		case SpecialBallType.CHAINSAW:
			logic = new PW_ChainSaw();
			break;
		case SpecialBallType.PAINT:
			logic = new PW_Paint();
			break;
		case SpecialBallType.ROLLBACK:
			logic = new PW_RollBack();
			break;
		case SpecialBallType.SHOWER:
			PW_Shower showerLg = new PW_Shower();
			showerLg.ColliderCallback = mTrigerCallBack;
			logic = showerLg;
			break;
		default:
			break;
		}

		return logic;
	}
}

