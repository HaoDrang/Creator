using UnityEngine;

namespace Game.Logic
{
	public class GridUtils
	{
		public static void SetRowPosInGrid(Row r, Grid g)
		{
			Transform rowTrans	= r.transform;
			Transform gridTrans = g.transform;
			rowTrans.SetParent (gridTrans);
			rowTrans.localPosition = Vector3.zero;
			rowTrans.localScale = Vector3.one;
		}

		public static float GetLongMoveDistance (int i)
		{
			throw new System.NotImplementedException ();
		}
	}
}

