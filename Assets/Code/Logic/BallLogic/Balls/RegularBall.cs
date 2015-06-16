using UnityEngine;

public class RegularBall : Ball
{
	const string ImgPrefix_Big 		= "B_";	//means big
	const string ImgPrefix_Small 	= "S_";	//means small
	const string PrefabName			= "RegularBall";
	protected int miColor = 0;
	protected static Color[] mColors = null;
	[SerializeField]
	protected ShakeBall mShake = null;

	public static Color[] ColorArray {
		set {
			mColors = value;
		}
	}

	public void Init (BallRow row, int c)
	{
		this.miColor = c;
		base.Init (row);
	}

	protected override void SetState (BallState state)
	{
		base.SetState (state);
		UpdateColor ();
		mImage.SetNativeSize ();
	}

	public static Ball Create (BallRow ballRow, int ballColor)
	{
		GameObject ball = PrefabMgr.Ins.CreateCopy (PrefabName);
//		ball.layer = Layers.StaticBall;
		ball.name = PrefabName + "_" + ballColor.ToString();
		RegularBall b = ball.GetComponent<RegularBall> ();
		b.Init (ballRow, ballColor);
		return b;
	}

	public override int GetBallTypeCode ()
	{
		return base.GetBallTypeCode () * BallDefines.BALL_TYPE_SPLITER + (int)miColor;
	}
	
	virtual public int GetColor ()
	{
		return miColor;
	}

	virtual public void SetBallColor (int c)
	{
		miColor = c;
		UpdateColor ();
	}

	protected void UpdateColor ()
	{
		mImage.color =  mColors[miColor];
	}

	public override void ConsumeFade ()
	{
		//create an effect here
		//GameObject consumeEffect = PrefabMgr.Instance.CreateCopy (BallDefines.REGULAR_BALL_CONSUME_EFFECT_NAME);
		Destroy (gameObject);
	}

	public override void Fall (float delay)
	{
		mRow.UnlinkBall (miBallIndex);
		Invoke ("Shake", delay);

	}
	void Shake()
	{
		mShake.Shake (ShakeDone);
	}

	void ShakeDone()
	{
		base.Fall (0);
	}
}

