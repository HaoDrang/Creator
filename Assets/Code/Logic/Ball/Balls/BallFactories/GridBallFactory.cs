namespace Game.Logic
{
	public class GridBallFactory : BallFactory
	{
		private int miPowerUpCount;
		private bool mbFirstPowerup;
		public GridBallFactory (LevelConfig conf) : base(conf)
		{
			miPowerUpCount = 0;
			mbFirstPowerup = true;
		}

		public override void Init (LevelConfig conf)
		{
			base.Init (conf);
		}

		public Ball GenerateBall(bool useDummies, bool hasPowerUps = false)
		{
			if (useDummies) {
				return MakeNormalBall();
			}
			
			int emptyOdds = UnityEngine.Random.Range (0, 100);

			if (hasPowerUps) {
				if (miPowerUpCount == 0) {
					var delay = mbFirstPowerup ? mLevelConfig.mPowerUpDelayInitial : mLevelConfig.mPowerUpDelay;
					miPowerUpCount = UnityEngine.Random.Range (delay [0], delay [1] + 1);
					mbFirstPowerup = false;
				}
				
				if (--miPowerUpCount <= 0) {
					return MakeSuperBall();
				}
			}
			
			return MakeNormalBall();
		}

		private Ball MakeNormalBall()
		{
			NormalBall ball = Generate<NormalBall> ();
			//setup normal ball
			int n = UnityEngine.Random.Range (0, mLevelConfig.miColorCount);
			ball.Init (mLevelConfig);
			ball.SetColor (n);
			ball.SetState (BallState.STATIC);
			return ball;
		}
		
		private Ball MakeSuperBall()
		{
			SuperBall ball = Generate<SuperBall> ();
			//setup super ball
			int n = UnityEngine.Random.Range (0, mLevelConfig.meSuperBallTypes.Length);
			ball.Init (mLevelConfig);
			ball.SetSuperType (mLevelConfig.meSuperBallTypes[n]);
			return null;
		}
	}
}

