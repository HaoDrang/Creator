using System;
using UnityEngine.EventSystems;

public interface IGameOverEventHandler : IEventSystemHandler
{
	void BallOutOfBoard();
}