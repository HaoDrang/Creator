using UnityEngine;
using System.Collections;
using Game.Logic;

public class RowMoveDownTest : MonoBehaviour {

	public Row r = null;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnButtonClick()
	{
		r.Move (10);
	}
}
