using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FPSWatcher : MonoBehaviour 
{
	private Text t = null;
	void Awake()
	{
		t = GetComponent<Text> ();
	}

	void Update()
	{
		t.text = "FPS:" + (1f / Time.deltaTime);
	}
}
