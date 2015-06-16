using FrameWork;
namespace Game.Logic
{
	public class BallFactory : TFactory<Ball>
	{
		private LevelConfig mLevelConfig;
		private const string msNormalBallKey 	= "NormalBall";
		private const string msSuperBallKey 	= "SuperBall";
		public BallFactory(LevelConfig conf)
		{
			mLevelConfig = conf;

			Init (conf);
		}

		void Init (LevelConfig conf)
		{
			Register<NormalBall> (()=>{
				UnityEngine.GameObject obj = PrefabMgr.Ins.CreateCopy(msNormalBallKey);
				return obj.GetComponent<NormalBall>();
			});

			Register<SuperBall> (()=>{
				UnityEngine.GameObject obj = PrefabMgr.Ins.CreateCopy(msSuperBallKey);
				return obj.GetComponent<SuperBall>();
			});
		}
	}
}

