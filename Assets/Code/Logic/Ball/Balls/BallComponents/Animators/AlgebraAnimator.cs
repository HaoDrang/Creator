using UnityEngine;
using Game.Logic.Clip;
using System;

namespace Game.Logic
{
	public class AlgebraAnimator : MonoBehaviour
	{
		protected static ClipFactory _factory = null;

		protected static ClipFactory ClipGenerator {
			get {
				if (_factory == null) {
					_factory = new ClipFactory ();
				}
				return _factory;
			}
		}

		private ActClip currentClip = null;
		virtual public void Start()
		{

		}

		virtual public void Play (ClipEnum clipEnum)
		{
			if (currentClip != null) {
				currentClip.Reset ();
				currentClip = null;
			}

			currentClip = GetCurrentClip (clipEnum);

			if (currentClip != null) {
				currentClip.Play ();
			}
		}

		virtual protected ActClip GetCurrentClip (ClipEnum clipEnum)
		{
			return null;
		}


		private void Update ()
		{
			if (currentClip != null) {
				currentClip.Tick (Time.deltaTime);
			}
		}
	}
}

