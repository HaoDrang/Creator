using UnityEngine;
using Game.Logic.Clip;
using System;


namespace Game.Logic
{
	[RequireComponent(typeof(RowAnimator))]
	public class Row : MonoBehaviour
	{
		private RowAnimator _animator = null;
		private LevelConfig _config = null;
		private int miNum = 0;
		void Awake()
		{
			_animator = GetComponent<RowAnimator> ();
		}

		void Start()
		{
			_animator.PushEvent.AddListener (PushDone);
		}

		public void Init (LevelConfig conf)
		{
			_config = conf;
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

		public void Fill(BallFactory generator)
		{
			//temp create
			// use temp create function instead
			//infact we put the manager into generator so the manager could decouple with the Row
			for (int i = 0; i < 20; i++) {
				GameObject obj = PrefabMgr.Ins.CreateCopy ("NormalBall");
				obj.name = "test object";
				obj.transform.SetParent (transform);
			}

			print ("Fill Row Ready");
		}
	}
}
