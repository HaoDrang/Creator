using UnityEngine;
using Game.Logic.Events;

namespace Game.Logic
{
	public class BallEventDispatcher : MonoBehaviour
	{
		[SerializeField]
		protected BallCollisionEvent
			_CollisionEvt = new BallCollisionEvent ();
		[SerializeField]
		protected BallShootEvent
			_ShootEvt = new BallShootEvent ();
//		[SerializeField]


		void OnCollisionEnter2D (Collision2D target)
		{
			if (_CollisionEvt != null) {
				_CollisionEvt.Invoke (gameObject, target.gameObject);
			}
		}

		void OnShootEvent (float angle, Ball ball)
		{
			if (_ShootEvt != null) {
				_ShootEvt.Invoke (angle, ball);
			}
		}
	}
}

