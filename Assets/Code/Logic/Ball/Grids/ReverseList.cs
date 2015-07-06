using System.Collections.Generic;

namespace Game.Logic
{
	public class ReverseList : Stack<Row>
	{
		public void Add(Row r)
		{
			base.Push (r);
			DisableRow (r);
		}

		public Row Get()
		{
			Row r = base.Pop ();
			EnableRow (r);
			return r;
		}

		void DisableRow(Row r)
		{
			r.gameObject.SetActive (false);
		}

		void EnableRow(Row r)
		{
			r.gameObject.SetActive (true);
		}
	}
}

