using UnityEngine.UI;
using UnityEngine;
using System;

namespace Game.Logic
{
	public class BallAppearance
	{
		protected Image[] _imgs = null;

		protected Image[] Imgs {
			get { 
				if (_imgs == null) {
					_imgs = new Image[(int)BLR.LayerNum];
				}
				return _imgs;
			}
		}

		public BallAppearance(Image[] images)
		{
			if (images != null) {
				int copyLength = images.Length < (int)BLR.LayerNum ? images.Length : (int)BLR.LayerNum;
				Array.Copy(images, Imgs, copyLength);
			}
		}

		public BallAppearance (Image BaseLayer, Image HighlightLayer = null,
		                      Image ShadowLayer = null, Image VeinLayer = null,
		                      Image AnmLayer = null, Image FrontLayer = null)
		{
			SetLayer (BLR.Front, FrontLayer);
			SetLayer (BLR.Highlight, HighlightLayer);
			SetLayer (BLR.Shadow, ShadowLayer);
			SetLayer (BLR.Vein, VeinLayer);
			SetLayer (BLR.SpecialAnimation, AnmLayer);
			SetLayer (BLR.BaseColor, BaseLayer);
		}

		protected void SetLayer (BLR eLayer, Image layerEntity)
		{
			Imgs [(int)eLayer] = layerEntity;
		}

		public BallAppearance Decorate (BLR eLayer, Sprite sp)
		{
			if (Imgs [(int)eLayer] != null) {
				if (sp != null) {
					Imgs [(int)eLayer].enabled = true;
					Imgs [(int)eLayer].sprite = sp;
				}
				else
				{
					Imgs [(int)eLayer].enabled = false;
				}
			}

			return this;
		}

		public void Done()
		{
			for (int i = 0; i < Imgs.Length; i++) {
				Image img = Imgs[i];
				if (img != null) {
					if (img.sprite == null) {
						img.enabled = false;
					}
				}
			}
		}

		public BallAppearance DecorateWithAnimation(BLR eLayer, string animationName)
		{
			if (Imgs [(int)eLayer] != null) {
				Animator anm = Imgs [(int)eLayer].GetComponent<Animator> ();
				if (anm != null) 
				{
					anm.Play (animationName);
				}
			}

			return this;
		}

		public BallAppearance SetColor(BLR eLayer, Color clr)
		{
			if (Imgs [(int)eLayer] != null) {
				Imgs [(int)eLayer].color = clr;
			}
			return this;
		}
	}

	public enum BLR//Ball Appreance Layer
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