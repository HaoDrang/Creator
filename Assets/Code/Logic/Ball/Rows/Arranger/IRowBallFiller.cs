namespace Game.Logic
{
	public interface IRowBallFiller
	{
		void CreateBall (Row curRow, GridBallFactory generator, LevelConfig conf, IRowArrangeBall arrange);
	}
}

