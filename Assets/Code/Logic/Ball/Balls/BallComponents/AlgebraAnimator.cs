using UnityEngine;
using Game.Logic.Clip;
using System;

namespace Game.Logic
{
	public class AlgebraAnimator : MonoBehaviour
	{
		private static ClipFactory _factory = null;

		protected static ClipFactory ClipGenerator {
			get {
				if (_factory == null) {
					_factory = new ClipFactory ();
				}
				return _factory;
			}
		}

		private ActClip currentClip = null;

		public void Start()
		{
			if (_factory == null) {
				ClipGenerator.Register<BallShakeClip>(()=>new BallShakeClip());
			}
		}

		public void Play (ClipEnum clipEnum)
		{
			if (currentClip != null) {
				currentClip.Reset ();
				currentClip = null;
			}
			switch (clipEnum) {
			case ClipEnum.BallShake:
				currentClip = ClipGenerator.Generate<BallShakeClip>();
				break;
			default:
				break;
			}

			if (currentClip != null) {
				currentClip.Play ();
			}
		}

		void Update ()
		{
			if (currentClip != null) {
				currentClip.Tick (Time.deltaTime);
			}
		}
	}
}

