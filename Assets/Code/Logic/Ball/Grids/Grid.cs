using UnityEngine;

namespace Game.Logic
{
	public class Grid : MonoBehaviour
	{
		protected LevelConfig _config;

		public void Init(LevelConfig conf)
		{
			_config = conf;
		}

		virtual public void Reset()
		{

		}

		virtual public void AddRow(int num)
		{
			//todo 
		}

		virtual public void AddRow()
		{

		}
	}
}

