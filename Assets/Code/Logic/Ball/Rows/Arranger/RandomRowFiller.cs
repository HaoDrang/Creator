using System.Collections.Generic;

namespace Game.Logic
{
	public class RandomRowFiller : IRowBallFiller
	{
		private Row mPreRow = null;
		public void SetPreRow(Row r)
		{
			mPreRow = r;
		}

		#region IRowBallCreate implementation
		void IRowBallFiller.CreateBall (Row curRow, GridBallFactory generator, LevelConfig conf, IRowArrangeBall arrange)
		{
			UnityEngine.Transform trans = curRow.transform;
			int emptyChance = conf.mEmptyOdds;
			int initTo = conf.mWidth [1];
			int initFrom = conf.mWidth [0];
			//create balls by chance
			int slotNum = initTo - initFrom;
			int ballNum = slotNum * (100 - emptyChance) / 100;
			
			Ball[] balls = new Ball[ballNum];
			for (int i = 0; i < ballNum; i++) {
				balls [i] = generator.GenerateBall(false, conf.meSuperBallTypes.Length > 0);// MakeGridBall (conf, bUseDummies, 0, true, hasPowerUps);
			}
			
			//set them to correct positions
			if (mPreRow == null) { //if this row is the first row 
				int[] ballIndexList = new int[slotNum];
				for (int i = 0; i < ballIndexList.Length; i++) {
					ballIndexList [i] = i + initFrom;
				}
				
				ArrayTool.Shuffle<int> (ballIndexList);
				
				for (int i = 0; i < ballNum; i++) {
					arrange.Arrange(balls[i].gameObject, trans, i);
				}
			} else {
				// create target number of ball
				// make target slots
				bool[] targetSlots = new bool[initTo - initFrom];
				// put them into target slots
				List<int> mTarIdx = new List<int> ();
				List<int> mScdIdx = new List<int> ();
				for (int i = 0; i < Configs.ROW_MAX_BALLS; i++) {
					// go through the pre row
					Ball ball = mPreRow.GetBall (i);
					if (ball != null) {
						int slotIdx = i - initFrom;//(0---targetSlots.Length)
						if ((i >= initFrom) && (i < initTo)) {
							targetSlots [slotIdx] = !targetSlots [slotIdx];
						}
						if (mPreRow.IsOffset) {
							if ((i + 1) >= initFrom && (i + 1 < initTo)) {
								if (slotIdx + 1 < targetSlots.Length) {
									targetSlots [slotIdx + 1] = !targetSlots [slotIdx + 1];
								}
							}
						} else {
                            if ((i - 1) >= initFrom && (i - 1 < initTo)) {
                                if (slotIdx - 1 >= 0) {
                                    targetSlots [slotIdx - 1] = !targetSlots [slotIdx - 1];
                                }
                            }
                        }
                    }
                }
                
                for (int k = 0; k < targetSlots.Length; k++) {
                    if (targetSlots [k]) {
                        mTarIdx.Add (k + initFrom);
                    } else {
                        mScdIdx.Add (k + initFrom);
                    }
                }
                
                // wash both of the lists
                ArrayTool.Shuffle<int> (mTarIdx);
                ArrayTool.Shuffle<int> (mScdIdx);
                mTarIdx.AddRange (mScdIdx);
                
                for (int j = 0; j < mTarIdx.Count; j++) {
                    if (j >= ballNum) {
						break;
					}
					arrange.Arrange(balls [j].gameObject, trans, j);
					//TODO  make other properties right
				}
			}
		}
		#endregion
	}
}

