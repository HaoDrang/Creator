using System;
using UnityEngine.EventSystems;

public interface IGameInfoEventHandler : IEventSystemHandler
{
	void FallBalls (int fallCount);
	void BallCollected(BallType bType, int param);
}