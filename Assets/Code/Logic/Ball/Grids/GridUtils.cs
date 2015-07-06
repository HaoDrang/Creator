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
//			UnityEngine.MonoBehaviour.print ("move forward! " + i);
			return Configs.BALL_SIZE.y * (i + 1);
			//throw new System.NotImplementedException ();
		}

		public static void SetRowPreparePos (Row r, int i)
		{
			Vector3 pos = r.transform.localPosition;
			pos.y += Configs.BALL_SIZE.y * i;
			r.transform.localPosition = pos;
		}
	}
}

