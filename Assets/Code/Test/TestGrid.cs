using UnityEngine;
using System.Collections;

public class TestGrid : MonoBehaviour {
	public Game.Logic.Grid grid = null;
	public LevelConfig _config;
	// Use this for initialization
	void Start () {
		grid.Init (_config);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnClick()
	{
		AddRow ();
	}

	void AddRow()
	{
		grid.AddRow (10);
	}

	void CreateNewRow()
	{
		grid.AddRow (null, true);
	}
}
