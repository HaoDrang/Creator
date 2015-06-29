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
		static private RowArrangerProvider _arranger = null;
		static private RowFillerProvider _filler = null;
		void Awake()
		{
			_animator = GetComponent<RowAnimator> ();
            _arranger = new RowArrangerProvider();
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

		public void Fill(GridBallFactory generator) //++ up to 30ms
		{
			//var tn = System.DateTime.Now;
			//print ("Begin Time: " + tn.Minute + ":" + tn.Second + ":" + tn.Millisecond);
			IRowArrangeBall arrange = _arranger.GetArranger<RegularRowArranger> ();
			Transform selfTrans = transform;
			GameObject newBallObj = null;

			IRowBallFiller filler = _filler.GetFiller<RandomRowFiller>();

			filler.CreateRandomBalls (transform, generator, _config, arrange);
//			for (int i = 0; i < 20; i++) {//TODO change it to config
//				Ball b = generator.GenerateBall(false);
//				newBallObj = b.gameObject;
//				arrange.Arrange(newBallObj, selfTrans, i);
//			}
			//tn = System.DateTime.Now;
			//print ("Begin Time: " + tn.Minute + ":" + tn.Second + ":" + tn.Millisecond);
			
			//print ("Fill Row Ready");
		}
	}
}
