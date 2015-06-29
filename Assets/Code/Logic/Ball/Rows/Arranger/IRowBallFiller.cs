namespace Game.Logic
{
	public interface IRowBallFiller
	{
		void CreateRandomBalls (UnityEngine.Transform trans,GridBallFactory generator, LevelConfig conf, IRowArrangeBall arrange);
	}
}

