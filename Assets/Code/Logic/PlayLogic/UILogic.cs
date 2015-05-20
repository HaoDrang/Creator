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
}
