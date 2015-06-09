using System;
using UnityEngine;
namespace Game.Logic
{
	public class Ball : MonoBehaviour, IRowHandler
	{
		protected BallState mState = default(BallState);

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

