using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextCreator : MonoBehaviour
{
	public void CreateConsumeText(int iCount, Color cColor, Vector3 targetPos)
	{
		GameObject ctobj = PrefabMgr.Ins.CreateCopy(BallDefines.CONSUME_COUNT_PREFAB_NAME);
		ctobj.transform.SetParent(transform);
		Vector3 pos = transform.InverseTransformPoint(targetPos);
		pos.z = 0;
		ctobj.transform.localPosition = pos;
		ctobj.transform.localScale = Vector3.one;
		ConsumeText ct = ctobj.GetComponent<ConsumeText>();
		ct.Init(iCount, cColor);
	}

	public void CreateDropCountText(int iCount, Vector3 targetPos)
	{
		GameObject dctobj = PrefabMgr.Ins.CreateCopy(BallDefines.DROP_COUNT_PREFAB_NAME);
		dctobj.transform.SetParent(transform);
		targetPos.z = 0;
		//		ctobj.transform.localPosition = targetPos; // TODO test if the movable position is better
		dctobj.transform.localPosition = Vector3.zero;
		dctobj.transform.localScale = Vector3.one;
		DropText dt = dctobj.GetComponent<DropText>();
		dt.Init(iCount);
	}
}

