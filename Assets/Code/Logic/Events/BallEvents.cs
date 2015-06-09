using System;
namespace Game.Logic.Events
{
	[Serializable]
	public class BallCollideEvent:UnityEngine.Events.UnityEvent<UnityEngine.GameObject,UnityEngine.GameObject>{}
	public class BallShootEvent:UnityEngine.Events.UnityEvent<float, int, int>{}//angle, balltype, type param
}

