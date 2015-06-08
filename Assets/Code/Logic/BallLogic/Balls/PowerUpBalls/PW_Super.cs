
using UnityEngine;

public class PW_Super : PowerUpLogicBase
{
	private int mBasePower = 10;
	public override void HandleCollidOtherBall(BallDisposer mBallListController, 
	                                           UnityEngine.GameObject shooted, 
	                                           UnityEngine.GameObject stable)
	{
		Debug.Log ("~~~" + stable.name);
	}

	public override bool HandleColledBorder(UnityEngine.GameObject ballObject, 
	                                        UnityEngine.GameObject border)
	{
		return true;
	}
}