using UnityEngine;
using Game.Logic.Clip;


namespace Game.Logic
{
	[RequireComponent(typeof(RowAnimator))]
	public class Row : MonoBehaviour
	{
		private RowAnimator _animator = null;

		void Awake()
		{
			_animator = GetComponent<RowAnimator> ();
		}

		public void Move (int iStep)
		{
			_animator.Move (iStep);
		}
	}
}
