using System;
using UnityEngine;
using UnityEngine.UI;
using Game.Logic.Events;

namespace Game.Logic
{
	public class Ball : MonoBehaviour, IRowHandler
	{
		[SerializeField]
		protected BallCollideEvent collideEvt = null;
		[SerializeField]
		protected Image[] _ballimgs = new Image[(int)BLR.LayerNum];

		protected BallState mState = default(BallState);
		protected BallAppearance _appearance = null;

		virtual public void Awake()
		{
			// default appearance
			_appearance = new BallAppearance (_ballimgs);
        }
        
		virtual public void DetachRow()
		{
			throw new NotImplementedException();
		}

		virtual public void SetRow(Row r)
		{
			throw new NotImplementedException();
		}

		virtual public void SetLayerImgs (UnityEngine.UI.Image[] images)
		{
			_ballimgs = images;
		}
 	}

	public enum BallState
	{
		PREPARE,
		READY,
		STATIC,
		FLY,
	}
}

