using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic
{
    [RequireComponent(typeof(AlgebraAnimator))]
	public class BallController : MonoBehaviour
	{
		[SerializeField]
		protected AlgebraAnimator _animator = null;
		[SerializeField]
		protected Image[] _ballimgs = new Image[(int)BLR.LayerNum];
		[SerializeField]
		protected Transform _layerroot = null;

		protected BallAppearance _appearance = null;

		virtual protected void Awake()
		{
			// default appearance
			_appearance = new BallAppearance (_ballimgs);
			_animator 	= GetComponent<AlgebraAnimator>();
		}
		
		virtual public void SetLayerImgs (UnityEngine.UI.Image[] images)
		{
			_ballimgs = images;
		}

		virtual public void Decorate(LevelConfig conf)
		{
			BallAppearanceLevel bal = (BallAppearanceLevel)conf.mDifficult;
			string nm = "";
			Sprite sp = null;
			for (int i = 0; i < (int)BLR.LayerNum; i++) {
				nm = GetDecorationName((BLR)i, bal);
				sp = SpritesMgr.Instance.GetImage(nm);
				_appearance.Decorate((BLR)i, sp);
			}
		}

		private string GetDecorationName (BLR ly, BallAppearanceLevel lv)
		{
			string ret = "";
			switch (ly) {
			case BLR.BaseColor:
				ret = BLR.BaseColor.ToString() + "_" + (int)BallAppearanceLevel.None;
				break;
			default:
				ret = BLR.BaseColor.ToString() + "_" + (int)lv;
				break;
			}

			return ret;
		}
	}
}

