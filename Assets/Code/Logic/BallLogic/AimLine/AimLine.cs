using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimLine : MonoBehaviour {

	private enum AimDirction 
	{
		M,
		L,
		R,
	}

	[SerializeField]
	private Rect GameBounds;
	[SerializeField]
	private List<LineRenderer> 	lines 			= new List<LineRenderer>();
	[SerializeField]
	private float 				startWidth		= 0.5f;
	[SerializeField]
	private float				endWidth		= 0.01f;
	private List<Vector3> 		targetPoints 	= new List<Vector3>();
	private float 				mLineDepth		= -5f;
	private float				mLeft			= 0f;
	private float				mRight			= 0f;
	private float 				mTop			= 0f;
	private float				mBottom			= 0f;
	void Start()
	{
		mLeft 	= GameBounds.x - GameBounds.width / 2f;
		mRight 	= GameBounds.x + GameBounds.width / 2f;
		mTop 	= GameBounds.y + GameBounds.height;
		mBottom = GameBounds.y;
	}

	public void AimTo (Vector3 position, float angle)
	{
		Transform trans = transform;
		Vector3 pt_0 = trans.InverseTransformPoint(position);
		pt_0.z = mLineDepth;

		AimDirction dir = AimDirction.M;

		if (targetPoints.Count < 1) 
		{
			targetPoints.Add(pt_0);
		}

		Vector3 pt_cur = targetPoints[0];
		Vector3 pt_nxt = default(Vector3);
		Vector3 pt_1 = new Vector3 ();
		int ptCount = 1;

		for (int repeat = 0; repeat < 20; repeat++) {
			bool hasNextPoint = GetNextPoint(angle, pt_cur,ref pt_nxt, ref dir);
			pt_nxt.z = mLineDepth;
			ptCount++;
			if (targetPoints.Count < ptCount) {
				targetPoints.Add(pt_nxt);
			}
			else
			{
				targetPoints[ptCount - 1] = pt_nxt;
            }
            pt_cur = pt_nxt;

            if (!hasNextPoint) {
				break;
			}
		}
		float addWidth = (startWidth - endWidth)/(ptCount - 1);

		for (int i = 0; i < ptCount - 1; i++) {
			if (i >= lines.Count) {
				lines.Add(GameObject.Instantiate<GameObject>(lines[0].gameObject).GetComponent<LineRenderer>());
				lines[i].transform.SetParent(lines[0].transform.parent);
				lines[i].transform.localScale = Vector3.one;
				lines[i].transform.localPosition = new Vector3(0,0,0);
			}
			lines [i].SetPosition (0, targetPoints[i]);
			lines [i].SetPosition (1, targetPoints[i + 1]);
			lines [i].SetWidth(startWidth - i * addWidth, startWidth - (i + 1) * addWidth);
			lines [i].gameObject.SetActive(true);
		}
		if (ptCount - 1 < lines.Count) {
			for (int j = ptCount - 1; j < lines.Count; j++) {
				lines [j].gameObject.SetActive(false);
			}
		}

//		Debug.Log ("~~~~~~~~~0:" + angle);

	}

	bool GetNextPoint (float orginAngle, Vector3 pt_current, ref Vector3 pt_next, ref AimDirction direction)
	{
		switch (direction) {
		case AimDirction.M:
			if (orginAngle < 90f) {
				float radius = Mathf.Deg2Rad * (orginAngle);
				pt_next = new Vector3 (mRight , mRight * Mathf.Tan (radius));
				if (pt_next.y >= mTop) 
				{
					pt_next.y = mTop;
					pt_next.x = pt_next.y / Mathf.Tan(radius);
				}
				direction = AimDirction.R;
			} else if (orginAngle == 90f) {
				pt_next = new Vector3 (0 , mTop);
			} else {//orginAngle > 90f
				float radius = Mathf.Deg2Rad * (orginAngle - 90f);
				pt_next = new Vector3 (mLeft , mRight / Mathf.Tan (radius));
				if (pt_next.y >= mTop) 
				{
					pt_next.y = mTop;
					pt_next.x =  - pt_next.y * Mathf.Tan(radius);
				}
				direction = AimDirction.L;
			}
			break;
		case AimDirction.L:
			float calAngleL = orginAngle > 90 ? (180f - orginAngle) : orginAngle;
			float radiusL = Mathf.Deg2Rad * calAngleL;
			pt_next = new Vector3 (mRight , pt_current.y + GameBounds.width * Mathf.Tan (radiusL), mLineDepth);
			if (pt_next.y >= mTop) 
			{
				pt_next.x = mLeft + (mTop - pt_current.y) / Mathf.Tan(radiusL);
				pt_next.y = mTop;
			}
			direction = AimDirction.R;
			break;
		case AimDirction.R:
			float calAngleR = orginAngle > 90 ? (180f - orginAngle) : orginAngle;
			float radiusR = Mathf.Deg2Rad * calAngleR;
			pt_next = new Vector3 (mLeft , pt_current.y + GameBounds.width * Mathf.Tan (radiusR));
			if (pt_next.y >= mTop) 
			{
				pt_next.x = mRight - (mTop - pt_current.y) / Mathf.Tan(radiusR);
				pt_next.y = mTop;
			}
			direction = AimDirction.L;
			break;
		default:
			break;
		}


//		Debug.Log ("GetNextPoint: Angle:" + orginAngle + ":CurrentPoint:" + pt_current + ":NextPoint:" + pt_next);
		if (pt_next.y >= mTop) 
		{
//			Debug.Log("~~~~" + pt_next);
			return false;
		}

		return true;
	}

	private void UpdateLinePoints()
	{
	}

	private void UpdateLimitedLinePoints()
	{

	}
}
