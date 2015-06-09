using UnityEngine;
using Game.Logic.Events;

namespace Game.Logic
{
	public class BallEventListener : MonoBehaviour
	{
		[SerializeField]
		protected BallCollideEvent
			_CollisionEvt = new BallCollideEvent ();
		[SerializeField]
		protected BallShootEvent
			_ShootEvt = new BallShootEvent ();

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

