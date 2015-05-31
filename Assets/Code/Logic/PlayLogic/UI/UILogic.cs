using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UILogic : MonoBehaviour 
{
	[SerializeField]
	private Text scorelb 	= null;
	[SerializeField]
	private Text steplb		= null;
	[SerializeField]
	private GameObject configDialog = null;
	[SerializeField]
	private GameObject gameRollUp = null;
	[SerializeField]
	private TextCreator tcreator = null;

	public void UpdateScore(int value)
	{
		if (scorelb != null) {
			scorelb.text = value.ToString();
		}
	}

	public void UpdateStep(int value)
	{
		if (steplb != null) {
			steplb.text = value.ToString();
		}
	}

	public void ShowConfig ()
	{
		if (configDialog != null) {
			configDialog.SetActive(!configDialog.activeSelf);
		}
	}

	public void ShowRollUp(bool bWin)
	{
		if (gameRollUp != null)	{
			gameRollUp.SetActive(!gameRollUp.activeSelf);
		}
	}

	public void CreateConsumeText(int iCount, Color ballColor, Vector3 vPos)
	{
		if (tcreator != null) {
			tcreator.CreateConsumeText(iCount, ballColor, vPos);
		}
	}

	public void CreateDropText(int iCount, Vector3 vPos)
	{
		if (tcreator != null) {
			tcreator.CreateDropCountText(iCount, vPos);
		}
	}
}
