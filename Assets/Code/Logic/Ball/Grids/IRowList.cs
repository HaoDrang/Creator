
using System;
using System.Collections.Generic;

namespace Game.Logic
{
	public interface IRowList
	{
		void Renumber();
		void AddTop(Row r);
		void AddBottom(Row r);
		void RemoveRow(Row r);	
		void RemoveRow(int index);
		Row  GetFirstRow();
		Row  GetLastRow();
		void Trim();
		void Move(int dir);
		void LongMove(float length);
		void Destroy();
	}
}

