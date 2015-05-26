using UnityEngine;
using System.Collections;

public class TestBaseDialog : MonoBehaviour 
{
	public GameObject dialogPrefab = null;
	void Start()
	{
		GameObject obj = GameObject.Instantiate<GameObject>(dialogPrefab);
		obj.transform.SetParent(transform);
		obj.transform.localScale = Vector3.one;
		obj.transform.localPosition = Vector3.zero;

		Dialog dia = obj.GetComponent<Dialog>();
		dia.Init("creator.welcomedialog");

//		dia.CreateButton("btnnext");

		obj.SetActive(true);
	}

}
