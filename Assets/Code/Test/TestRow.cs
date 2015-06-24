using UnityEngine;
using System.Collections;

public class TestRow : MonoBehaviour {

	public LevelConfig _config;

	public Game.Logic.Row r = null;
	// Use this for initialization
	void Start () {
		r.Init (_config);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FillRow()
	{
		r.Fill (null);
	}
}
