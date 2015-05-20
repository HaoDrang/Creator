using UnityEngine;

public class PowerUpLogicBase
{
	virtual public void Shoot (GameObject obj, Transform p, Vector3 worldPos, Quaternion worldRot, Vector2 velocity)
	{
		obj.layer = Layers.Ball;
		obj.transform.position = worldPos;
		obj.transform.SetParent (p);
		obj.transform.localScale = Vector3.one;
		obj.transform.rotation = worldRot;
		
		obj.GetComponent<Collider2D> ().isTrigger = false;
		obj.GetComponent<Rigidbody2D> ().gravityScale = 0f;
		obj.GetComponent<Rigidbody2D> ().velocity = velocity;
	}

	virtual public void HandleCollidOtherBall (BallDisposer mBallListController, GameObject shooted, GameObject stable)
	{

	}

	virtual public bool HandleColledBorder(GameObject ballObject, GameObject border)
	{
		bool ret = false;
		switch (border.tag) {
		case Tags.TOP_BORDER:
			ballObject.GetComponent<Ball>().ShootOut();
			ret = true;
			break;
		default:
			break;
		}
		return ret;
	}
}