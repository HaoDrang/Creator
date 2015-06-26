using UnityEngine;

namespace Game.Logic
{
	public class RegularRowArranger : IRowArrangeBall
	{
		#region IRowArrangeBall implementation
		GameObject IRowArrangeBall.Arrange (GameObject target, Transform parent, int pos)
		{
			Transform trans = target.transform;
			target.name = "Ball_" + pos;
			trans.SetParent (parent);
			Vector3 localPos = new Vector3 (pos * Configs.BALL_SIZE.x, 0, 0);
			trans.localPosition = localPos;
			trans.localScale = Vector3.one;
			return target;
		}
		#endregion
	}
}

