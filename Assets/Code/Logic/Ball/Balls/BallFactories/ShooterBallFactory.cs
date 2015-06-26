
namespace Game.Logic
{
	public class ShooterBallFactory : BallFactory
	{
		public ShooterBallFactory (LevelConfig conf) : base(conf)
		{
		}

		public override void Init (LevelConfig conf)
		{
			base.Init (conf);
		}
	}
}

