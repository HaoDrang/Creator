namespace Game.Logic
{
	public interface IRowBallFiller
	{
		void CreateRandomBalls (Row curRow, GridBallFactory generator, LevelConfig conf, IRowArrangeBall arrange);
	}
}

