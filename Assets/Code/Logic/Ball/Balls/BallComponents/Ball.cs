using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic
{
    [RequireComponent(typeof(BallEventDispatcher))]
    [RequireComponent(typeof(BallController))]
	public class Ball : MonoBehaviour, IRowHandler
	{
		[SerializeField]
		protected BallEventDispatcher _listener = null;
		[SerializeField]
		protected BallController _controller = null;

		protected BallState mState = default(BallState);

		protected Row mRow = null;

		virtual public void Awake()
		{
			_listener = GetComponent<BallEventDispatcher>();
			_controller = GetComponent<BallController> ();
        }
        
		virtual public void DetachRow()
		{
			mRow = null;
		}

		virtual public void SetRow(Row r)
		{
			mRow = r;
		}

		virtual public void SetState(BallState state)
		{
			BallState former = state;
			mState = state;
			if (former != mState) {
				_controller.StateChanged(former, mState);
			}
		}

		protected T GetController<T>() where T: BallController
		{
			return (T)_controller;
		}
 	}

	public enum BallState
	{
		PREPARE,
		READY,
		STATIC,
		MOVE,
		FLY,
		DISPOSED,
	}
}

