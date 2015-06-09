using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic
{
	public class Ball : MonoBehaviour, IRowHandler
	{
		[SerializeField]
		protected BallEventListener _listener = null;

		protected BallState mState = default(BallState);

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

