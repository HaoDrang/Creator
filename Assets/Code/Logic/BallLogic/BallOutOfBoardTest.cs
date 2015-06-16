using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;

public class BallOutOfBoardTest : MonoBehaviour {
	private GameObject mainObj = null;
	IEnumerator Start()
	{
		mainObj = GameObject.Find ("Main");
		yield return null;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
//		if (other.tag == Tags.BALL && other.gameObject.layer == Layers.StaticBall) 
//		{
//			ExecuteEvents.Execute<IGameOverEventHandler>(mainObj, null, BallOverLoad);
//		}
	}

	void BallOverLoad (IGameOverEventHandler handler, BaseEventData eventData)
	{
		handler.BallOutOfBoard ();
	}
}
