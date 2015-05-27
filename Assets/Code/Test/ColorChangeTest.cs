using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ColorChangeTest : MonoBehaviour 
{
	public Image img;
	public Color color;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (img != null) {
			img.color = color;
		}
	}
}
