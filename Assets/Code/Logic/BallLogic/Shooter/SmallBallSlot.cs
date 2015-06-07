using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SmallBallSlot : MonoBehaviour
{
	[SerializeField]
	private Image ballImg = null;
	private const string ImgPrefix_Big = "S_";	//means small
	private int mBallColor = 0;
	private BallType mBallType = BallType.Regular;
	private Color[] mCurrentColors = null;
	private Sprite NormalSprite = null;
	public void Init(Color[] clList)
	{
		mCurrentColors = clList;
	}

	void Awake()
	{
		NormalSprite = ballImg.sprite;
	}

	public void PrepareRandBall (int colorCount)
	{
		PrepareBall (Random.Range (0, colorCount));
	}

	public void PrepareRandPowerUpBall ()
	{
		mBallType = BallType.PowerUp;
		PrepareBall ((SpecialBallType)(Random.Range(0,(int)SpecialBallType.SUPERBALL)));
	}

	void PrepareBall (int c)
	{
		mBallType = BallType.Regular;
		Animator anm = GetComponent<Animator> ();
		anm.enabled = false;
		ballImg.sprite = NormalSprite;
		mBallColor = c;
		SetColor (c);
		if (!gameObject.activeSelf) {
			gameObject.SetActive (true);
		}
	}

	void PrepareBall (SpecialBallType t)
	{
		mBallType = BallType.PowerUp;
		Animator anm = GetComponent<Animator> ();
		anm.enabled = true;
		ballImg.color = Color.white;
		mBallColor = (int)t;
		anm.Play (ImgPrefix_Big + (int)t);
		if (!gameObject.activeSelf) {
			gameObject.SetActive (true);
		}
	}

	public void PrepareBall (BallType bt, int bColor)
	{
		mBallType = bt;
		mBallColor = bColor;
		switch (bt) {
		case BallType.Regular:
			PrepareBall (GetBallColor ());
			break;
		case BallType.PowerUp:
			PrepareBall (GetSpecialBallType ());
			break;
		default:
			break;
		}
	}

	int GetBallColor ()
	{
		return mBallColor;
	}

	SpecialBallType GetSpecialBallType ()
	{
		return (SpecialBallType)(mBallColor);
	}

	public void SetImage (int c)
	{
		ballImg.sprite = SpritesMgr.Instance.GetImage (ImgPrefix_Big + c.ToString ());
	}

	void SetColor (int c)
	{
		if (c >= 0 || c < mCurrentColors.Length) {
			ballImg.color = mCurrentColors [c];
		}
	}

	public void HideBall ()
	{
		gameObject.SetActive (false);
	}

	public int BallColorInfo	
	{ 
		get 
		{
			return mBallColor;
		} 
	}

	public BallType BallType  { get { return mBallType; } }

}

