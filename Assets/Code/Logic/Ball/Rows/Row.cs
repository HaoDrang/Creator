using UnityEngine;
using Game.Logic.Clip;
using System;


namespace Game.Logic
{
	[RequireComponent(typeof(RowAnimator))]
	public class Row : MonoBehaviour
	{
		private RowAnimator _animator = null;
		private int miNum = 0;
		void Awake()
		{
			_animator = GetComponent<RowAnimator> ();
		}

		void Start()
		{
			_animator.PushEvent.AddListener (PushDone);
		}

		public void SetNumber (int i)
		{
			miNum = i;
		}

		public void Move (int iStep)
		{
			_animator.Move (iStep);
		}

		public void LongMove(float length)
		{
			_animator.LongMove (length);
		}

		int tempCounter = 0;
		void PushDone()
		{
			Debug.Log ("Push Done" + tempCounter++);
		}

		public void Waste ()
		{
			throw new NotImplementedException ();
		}
	}
}
