using UnityEngine;
using Game.Logic.Clip;
namespace Game.Logic
{
	public class BallAnimator : MonoBehaviour
	{
		private static ClipFactory _ClipFactory = null;
		protected static ClipFactory ClipGenerator
		{
			get
			{
				if (_ClipFactory == null) {
					_ClipFactory = new ClipFactory();
				}
				return _ClipFactory;
			}
		}

		public void Play(ClipEnum clipEnum)
		{
			switch (clipEnum) {
			case ClipEnum.BallShake:
				break;
			default:
				break;
			}
		}
	}
}

