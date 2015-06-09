using UnityEngine;
namespace Game.Logic
{
	public class BallAnimatorController : MonoBehaviour
	{
		private Vector3 mvOriginPos = default(Vector3);
		//lets controll the animations all by hands
		public void SetOriginPosition(Vector3 pos)
		{
			mvOriginPos = pos;
		}

		public void SetOriginPosotion(Vector2 pos)
		{
			SetOriginPosition (new Vector3(pos.x, pos.y, 0));
		}
	}
}

