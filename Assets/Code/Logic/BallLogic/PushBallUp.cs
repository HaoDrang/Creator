using UnityEngine;
using System.Collections;

public class PushBallUp : MonoBehaviour {
	[SerializeField]
	private float pushForce = 500f;
	void OnTriggerEnter2D(Collider2D target)
	{
		if (target.tag == "BALL") {
			target.attachedRigidbody.velocity = new Vector2(0,0);
			target.attachedRigidbody.gravityScale = 0f;
			target.attachedRigidbody.AddForce(new Vector2(0,pushForce), ForceMode2D.Force);
		}
	}
}
