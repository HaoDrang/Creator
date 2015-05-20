using UnityEngine;
using System.Collections;

public class PreDataLoader : MonoBehaviour 
{
	[SerializeField]
	private string PropertyPath = "";
	public GameObject[] ToBeEnabled;
	IEnumerator Start()
	{
		Property.LoadDictionary(PropertyPath);
		yield return null;
		for (int i = 0; i < ToBeEnabled.Length; i++) {
			GameObject obj = ToBeEnabled[i];
			if (obj != null) {
				obj.SetActive(true);
			}
		}
		yield break;
	}
}
