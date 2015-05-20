using UnityEngine;
using System.Collections;

public class TestQuartation : MonoBehaviour {
	public Quaternion q = new Quaternion();
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = q;
	}
}
