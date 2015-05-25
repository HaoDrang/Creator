using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BigBallSlot : MonoBehaviour
{
	[SerializeField]
	private Image ballImg = null;
	[SerializeField]
	private Transform SlideTarget = null;
	[SerializeField]
	private float SlideTime = 0.2f;
	private float mTimeCounter = 0f;
	[SerializeField]
	private AnimationCurve Curve;
	[SerializeField]
	private float SlideFrom	= 0f;
	[SerializeField]
	private float SlideTo	= 0f;
	[SerializeField]
	private Animator BigSlotAnimator = null;
	private bool  mShow	= false;
	private const string ImgPrefix_Big 		= "B_";	//means big
	private int mBallColor = 0;
	private BallType mBallType = BallType.Regular;
	private Color[] mCurrentColors = null;
	public void Init (Color[] colors)
	{
		mCurrentColors = colors;
	}

	void Awake()
	{
		SlideTo = SlideTarget.localPosition.y;
	}

	void PrepareBall(int c)
	{
		BigSlotAnimator.enabled = false;
		mBallType = BallType.Regular;
		mBallColor = c;
		SetColor (c);
		SlideOutBall ();
		if (!gameObject.activeSelf) {
			gameObject.SetActive(true);
		}
	}

	void PrepareBall(SpecialBallType eType)
	{
		BigSlotAnimator.enabled = true;
		SlideOutBall ();
		BigSlotAnimator.Play (ImgPrefix_Big + (int)eType);
		if (!gameObject.activeSelf) {
			gameObject.SetActive(true);
		}
	}

	public void PrepareBall(BallType eType, int iType)
	{
		mBallType = eType;
		mBallColor = iType;
		switch (eType) {
		case BallType.Regular:
			PrepareBall(GetBallColor());
			break;
		case BallType.PowerUp:
			PrepareBall(GetSpecialBallType());
			break;
		default:
			break;
		}
	}

	int GetBallColor ()
	{
		return mBallColor;
	}
	
	SpecialBallType GetSpecialBallType()
	{
		return (SpecialBallType)(mBallColor);
	}

	public void SetImage(int c)
	{
		ballImg.sprite = SpritesMgr.Instance.GetImage(ImgPrefix_Big + ((BallColor)c).ToString());
	}

	void SetColor (int c)
	{
		if (c >= 0 || c < mCurrentColors.Length) {
			ballImg.color = mCurrentColors [c];
		}
	}

	public void HideBall()
	{
		gameObject.SetActive (false);
	}

	public void SlideOutBall()
	{
		Vector3 pos = SlideTarget.localPosition;
		pos.y = SlideFrom;
		SlideTarget.localPosition = pos;
		mShow = true;
		mTimeCounter = 0f;
	}

	public int BallColorInfo
	{
		get{
			return mBallColor;
		}
	}

	void Update()
	{
		if (mShow) {
			if (mTimeCounter <= SlideTime) 
			{
				mTimeCounter += Time.deltaTime;
				float curOffset = Curve.Evaluate(mTimeCounter / SlideTime) * (SlideTo - SlideFrom) + SlideFrom;
				Vector3 pos = SlideTarget.localPosition;
				pos.y = curOffset;
				SlideTarget.localPosition = pos;
			}
			else
			{
				mShow = false;
				mTimeCounter = 0f;
			}
		}
	}

	public BallType GetBallType ()
	{
		return mBallType;
	}
}