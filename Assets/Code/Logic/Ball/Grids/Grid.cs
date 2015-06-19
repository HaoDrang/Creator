using System;
using System.Collections;
using UnityEngine;

namespace Game.Logic
{
	public class Grid : MonoBehaviour
	{
		protected LevelConfig 	_config;
		protected RowList 		_rows;
		private RowFactory 		_rowFactory;
		protected bool mbCreatingRow = false;
		public void Init(LevelConfig conf)
		{
			_config = conf;
			_rows = new RowList ();
			_rowFactory = new RowFactory (_config);
		}

		virtual public void Reset()
		{
			_rows.Destroy ();
			_rows = new RowList ();
		}

		virtual public void AddRow(int num)
		{
			if (mbCreatingRow) {
				Debug.Log("try enter the AddRow during the function is processing");
				return;
			}
			StartCoroutine (CreateRowList(num));
		}

		virtual public void AddRow()
		{

		}

		protected IEnumerator CreateRowList(int num)
		{
			mbCreatingRow = true;
			yield return null;

			Row r = null;
			for (int i = 0; i < num; i++) {
				r = _rowFactory.Generate();
				yield return null;
			}

			mbCreatingRow = false;
			yield break;
		}
	}
}

