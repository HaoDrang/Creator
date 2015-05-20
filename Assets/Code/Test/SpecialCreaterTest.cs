using UnityEngine;
using System.Collections;

public class SpecialCreaterTest : MonoBehaviour {
	public SpecialBallType ballType = SpecialBallType.BOMB;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.F1)) {
			CreateSpecialBall();
		}
	}

	void CreateSpecialBall ()
	{
		Ball ball = SpecialBall.Create (null, ballType);
		ball.transform.SetParent (transform);
		ball.transform.localScale = Vector3.one;
		ball.transform.localPosition = Vector3.zero;
		ball.GetComponent<Rigidbody2D> ().isKinematic = false;
		ball.GetComponent<Rigidbody2D> ().gravityScale = 1f;
	}
}
