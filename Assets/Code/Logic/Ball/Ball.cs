using System;
using UnityEngine;
using UnityEngine.UI;
namespace Game.Logic
{
	public class Ball : MonoBehaviour, IRowHandler
	{
		protected BallState mState = default(BallState);
		protected BallAppearance _appearance = null;

		protected Image[] _ballimgs = new Image[(int)BLR.LayerNum];

		virtual public void Awake()
		{

		}

		virtual public void DetachRow()
		{
			throw new NotImplementedException();
		}

		virtual public void SetRow(Row r)
		{
			throw new NotImplementedException();
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

