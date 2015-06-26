using UnityEngine;
using System.Collections;
using Game.Logic;

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
		GridBallFactory fac = new GridBallFactory (_config);
		r.Fill (fac);
	}
}
