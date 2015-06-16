namespace Game.Logic
{
	public class Grid
	{
		protected LevelConfig _config;
//		protected Grid_RowFactory 
		public Grid(LevelConfig conf)
		{
			_config = conf;
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

