
namespace Game.Logic.Clip
{
	public class BallAnimator : AlgebraAnimator
	{
		public override void Start ()
		{
			base.Start ();
			if (_factory == null) {
				ClipGenerator.Register<BallShakeClip> (() => new BallShakeClip ());
			}
		}

		protected override ActClip GetCurrentClip (ClipEnum clipEnum)
		{
			ActClip ac = null;
			switch (clipEnum) {
			case ClipEnum.BallShake:
				ac = ClipGenerator.Generate<BallShakeClip> ();
				break;
			default:
				break;
			}
			return ac;
		}

		//TODO need a inherited play function
	}
}

