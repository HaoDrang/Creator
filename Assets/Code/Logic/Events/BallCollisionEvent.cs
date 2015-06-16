using System;
namespace Game.Logic.Events
{
	[Serializable]
	//------------------Ball Events------------------
	public class BallCollisionEvent:UnityEngine.Events.UnityEvent<UnityEngine.GameObject,UnityEngine.GameObject>{}//source, target
	[Serializable]
	public class BallShootEvent:UnityEngine.Events.UnityEvent<float, Ball>{}//angle, balltype, type param
	[Serializable]
	public class BallDisposeEvent:UnityEngine.Events.UnityEvent<Ball>{}//
	[Serializable]
	public class BallCollecteEvent:UnityEngine.Events.UnityEvent<Ball>{}//collect dropping balls
	[Serializable]
	public class BallOutOfScropeEvent:UnityEngine.Events.UnityEvent<Ball>{}//escape ball
	//-----------------------------------------------

	//----------------Shooter events-----------------
	[Serializable]
	public class ShooterShootEvent:UnityEngine.Events.UnityEvent<float>{} // shoot angle
	[Serializable]
	public class ShooterReadyEvent:UnityEngine.Events.UnityEvent{} // Shooter Cooled down
	//-----------------------------------------------

	//----------------Grid manager Evts--------------
	[Serializable]
	public class GridPushEvent:UnityEngine.Events.UnityEvent{} // push the grid
	[Serializable]
	public class GridPushDone:UnityEngine.Events.UnityEvent{}  // push done
	[Serializable]
	public class GridPullEvent:UnityEngine.Events.UnityEvent{} // pull up the grid
	[Serializable]
	public class GridPullDone:UnityEngine.Events.UnityEvent{}  // pull up grid done
	//-----------------------------------------------

	//----------------Game Events--------------------
	//-----------------------------------------------
	
}

