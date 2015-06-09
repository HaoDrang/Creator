using UnityEngine.UI;

namespace Game.Logic
{
	public class BallAppearance
	{
		protected Image[] _imgs = null;

		protected Image[] Imgs {
			get { 
				if (_imgs == null) {
					_imgs = new Image[(int)BALr.LayerNum];
				}
			}
		}

		public BallAppearance (Image BaseLayer, Image HighlightLayer = null,
		                      Image ShadowLayer = null, Image VeinLayer = null,
		                      Image AnmLayer = null, Image FrontLayer = null)
		{
			SetLayer (BALr.Front, FrontLayer);
			SetLayer (BALr.Highlight, HighlightLayer);
			SetLayer (BALr.Shadow, ShadowLayer);
			SetLayer (BALr.Vein, VeinLayer);
			SetLayer (BALr.SpecialAnimation, AnmLayer);
			SetLayer (BALr.BaseColor, BaseLayer);
		}

		protected void SetLayer (BALr eLayer, Image layerEntity)
		{
			Imgs [(int)eLayer] = layerEntity;
		}

		public BallAppearance Set ()
		{

		}
	}

	public enum BALr//Ball Appreance Layer
	{
		Front,
		Highlight,
		Shadow,
		Vein,
		SpecialAnimation,
		BaseColor,
		LayerNum,
	}
}