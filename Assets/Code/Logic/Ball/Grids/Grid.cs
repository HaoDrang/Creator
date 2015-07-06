using System;
using System.Collections;
using UnityEngine;

namespace Game.Logic
{
	public class Grid : MonoBehaviour
	{
		protected LevelConfig 	_config;
		protected RowList 		_rows;
		protected ReverseList   _reverse;
		private RowFactory 		_rowFactory;
		private GridBallFactory _ballFactory;
		protected bool mbCreatingRow = false;
		public void Init(LevelConfig conf)
		{
			_config = conf;
			_rows = new RowList ();
			_rowFactory = new RowFactory (_config);
			_ballFactory = new GridBallFactory (_config);
			_reverse = new ReverseList ();
		}

		virtual public void Reset()
		{
			_rows.Destroy ();
			_rows = new RowList ();
			_reverse.Clear ();
		}

		virtual public void AddRow(int num)
		{
			if (mbCreatingRow) {
				Debug.Log("try enter the AddRow during the function is processing");
				return;
			}
			StartCoroutine (CreateRowList(num));
		}

		virtual public void AddRow(Row r, bool bTop = false)
		{
			r.Init(_config);
			GridUtils.SetRowPosInGrid(r, this);
			if (bTop) {
				_rows.AddBottom(r);
			} else {
				_rows.AddTop(r);	
			}
			Renumber ();
		}

		protected IEnumerator CreateRowList(int num)
		{
			mbCreatingRow = true;
			yield return null;

			Row r = null;
			for (int i = 0; i < num; i++) {
				r = _rowFactory.Generate();
				r.Init(_config);
				r.Fill(_ballFactory);
				GridUtils.SetRowPosInGrid(r, this);
				GridUtils.SetRowPreparePos(r, i);
				r.LongMove(GridUtils.GetLongMoveDistance(num));
				_rows.AddBottom(r);
				yield return null;
			}
			Renumber ();
			mbCreatingRow = false;
			yield break;
		}

		protected void Renumber()
		{
			for (int i = 0; i < _rows.Count; i++) {
				_rows[i].SetNumber(i);
			}
		}
	}
}

