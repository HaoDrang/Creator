using UnityEngine;
using System.Collections;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]
public class Ball : MonoBehaviour 
{
	protected	BallRow mRow;
	protected int miBallIndex;
	protected int mSequenceID = 0;
	protected Image mImage = null;
	protected BoolDelegate_GObj_GObj mTrigerCallBack = null;
	protected BallType mBallType = BallType.Regular;
	[SerializeField]
	protected float FadeTime = 0.6f;
	private bool mFade = false;

	virtual protected void Awake()
	{
		mImage = GetComponent<Image> ();
	}
	
	virtual protected void Update()
	{
		if (mFade) {
			Color c = mImage.color;
			c.a -= Time.deltaTime / FadeTime;
			mImage.color = c;
			if (c.a <= 0) {
				Destroy (gameObject);
			}
		}
	}
	
	void OnCollisionEnter2D(Collision2D target)
	{
		if (mTrigerCallBack != null) 
		{
			if(mTrigerCallBack(gameObject, target.gameObject))
			{
				mTrigerCallBack = null;
			}
		}

//		Debug.Log ("Collision:" + target.contacts[0].point.ToString());
		GameObject obj = PrefabMgr.Instance.CreateCopy (BallDefines.BALL_SHOCKWAVE);
		obj.transform.position = new Vector3 (target.contacts[0].point.x, target.contacts[0].point.y, target.gameObject.transform.position.z - 2f);
	}

	virtual public void Init(BallRow row)
	{
		this.mRow = row;

		if (row != null) 
		{
			this.transform.SetParent (row.transform);
		}

		SetState (BallState.NORMAL);
	}

	virtual protected void SetState (BallState state)
	{
		//actually do nothing
	}

	public void SetBallRow (BallRow ballRow, int index)
	{
		mRow = ballRow;
		miBallIndex = index;
	}

	public int GetIndex()
	{
		return miBallIndex;
	}

	public BallRow GetBallRow()
	{
		return mRow;
	}
	
	public void SetLocalPos (int iX, int iY)
	{
		Vector3 vec = new Vector3 (iX, iY, 0);
		transform.localPosition = vec;
	}

	public void Waste ()
	{
		GetComponent<Collider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().isKinematic = false;
		mFade = true;
	}

	public int GetSequenceID ()
	{
		return mSequenceID;
	}

	public void SetSequenceID (int sequenceID)
	{
		mSequenceID = sequenceID;
	}


	virtual public int GetBallTypeCode()
	{
		return 0;
	}

	virtual public void Shoot(Transform p, Vector3 worldPos, Quaternion worldRot, 
	                          Vector2 velocity, BoolDelegate_GObj_GObj cb)
	{
		mTrigerCallBack = cb;
		gameObject.layer = Layers.Ball;
		transform.position = worldPos;
		transform.SetParent (p);
		transform.localScale = Vector3.one;
		transform.rotation = worldRot;

		GetComponent<Collider2D> ().isTrigger = false;
		GetComponent<Rigidbody2D> ().velocity = velocity;
	}

	public void DoFadeOut ()
	{
		if (mRow != null) {
			mRow.SetBall (null, miBallIndex);
		}
		Waste();
	}

	public void DoFadeOutAndBoom(float delayTime)
	{
		if (mRow != null) {
			mRow.SetBall (null, miBallIndex);
		}

		GetComponent<Collider2D> ().enabled = false;
		GetComponent<Rigidbody2D> ().isKinematic = false;

		Invoke ("FadeAndBoomImmediatly", delayTime);
	}

	void FadeAndBoomImmediatly()
	{

		GameObject boomObj = PrefabMgr.Instance.CreateCopy(BallDefines.BALL_BOOM_EFFECT_NAME);

		boomObj.transform.position = transform.position;
		boomObj.transform.localScale = Vector3.one;

		Destroy(boomObj, 1f);
		Waste();
	}

	public void ShootOut ()
	{
		//play a partical here
		Destroy (gameObject);
	}

	public BallType GetBallType()
	{
		return mBallType;
	}

	public void SetBallType(BallType bt)
	{
		mBallType = bt;
	}

	virtual public void Fall (float delay)
	{
		gameObject.layer = Layers.FallingBall;
		Rigidbody2D rb2d = GetComponent<Rigidbody2D> ();
		rb2d.isKinematic = false;
		rb2d.gravityScale = 1f;
		if (mRow != null) {
			mRow.UnlinkBall (miBallIndex);
		}
	}

	virtual public bool IsPowerUp()
	{
		return false;
	}

	public void Dispose ()
	{
		// Play a animation then delete self
		Destroy (gameObject);
	}

	public void SetColliderCallback (BoolDelegate_GObj_GObj mColliderCallback)
	{
		this.mTrigerCallBack = mColliderCallback;
	}

	virtual public void ConsumeFade ()
	{
	}
}
