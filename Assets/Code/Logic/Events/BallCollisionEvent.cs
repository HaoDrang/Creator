using System;
namespace Game.Logic.Events
{
	[Serializable]
	//------------------Ball Events------------------
	public class BallCollisionEvent:UnityEngine.Events.UnityEvent<UnityEngine.GameObject,UnityEngine.GameObject>{}//source, target
	public class BallShootEvent:UnityEngine.Events.UnityEvent<float, Ball>{}//angle, balltype, type param
	public class BallDisposeEvent:UnityEngine.Events.UnityEvent<Ball>{}//
	public class BallCollecteEvent:UnityEngine.Events.UnityEvent<Ball>{}//collect dropping balls
	public class BallOutOfScropeEvent:UnityEngine.Events.UnityEvent<Ball>{}//escape ball
	//-----------------------------------------------

	//----------------Shooter events-----------------
	public class ShooterShootEvent:UnityEngine.Events.UnityEvent<float>{} // shoot angle
	public class ShooterReadyEvent:UnityEngine.Events.UnityEvent{} // Shooter Cooled down
	//-----------------------------------------------

	//----------------Grid manager Evts--------------
	public class GridPushEvent:UnityEngine.Events.UnityEvent{} // push the grid
	public class GridPushDone:UnityEngine.Events.UnityEvent{}  // push done
	public class GridPullEvent:UnityEngine.Events.UnityEvent{} // pull up the grid
	public class GridPullDone:UnityEngine.Events.UnityEvent{}  // pull up grid done
	//-----------------------------------------------

	//----------------Game Events--------------------
	//-----------------------------------------------
	
}

