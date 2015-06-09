using UnityEngine;
using System.Collections;

public class TestShooter : MonoBehaviour {
	[SerializeField]
	BallShooter shooter = null;
	[SerializeField]
	GameObject ball = null;
	[SerializeField]
	Transform prepareSlot = null;
	[SerializeField]
	Transform BallLayer = null;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {

//Wasted
//			var pos = Input.mousePosition;
//			Debug.Log(pos.ToString());
//			pos.x -= Screen.width / 2f;
//			//pos.y += Screen.height / 2f;
//			GameObject newBall = Instantiate<GameObject>(ball);
//			newBall.transform.position = prepareSlot.position;
//			newBall.transform.SetParent(BallLayer);
//			newBall.transform.localScale = Vector3.one;
//			newBall.SetActive(true);
//			Vector2 localpoint = Vector2.zero;
//			RectTransformUtility.ScreenPointToLocalPointInRectangle(prepareSlot.GetComponent<RectTransform>(), new Vector2(pos.x, pos.y), Camera.current,out localpoint);
//			Debug.Log(localpoint.ToString());
//			shooter.Shoot(newBall, localpoint.normalized);
		}
	}
}
