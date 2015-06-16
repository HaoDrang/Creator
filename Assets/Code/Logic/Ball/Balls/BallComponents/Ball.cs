using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Logic
{
    [RequireComponent(typeof(BallEventListener))]
    [RequireComponent(typeof(BallController))]
	public class Ball : MonoBehaviour, IRowHandler
	{
		[SerializeField]
		protected BallEventListener _listener = null;
		[SerializeField]
		protected BallController _controller = null;

		protected BallState mState = default(BallState);

		protected Row mRow = null;

		virtual public void Awake()
		{
			_listener = GetComponent<BallEventListener>();
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

