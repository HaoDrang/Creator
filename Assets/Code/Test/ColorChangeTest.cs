using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
public class ColorChangeTest : MonoBehaviour 
{
	public Image img;
	public Color color;
	public int RepeatCount = 5000;
	Dictionary<System.Type, object> mDic = new Dictionary<Type, object>();
	// Use this for initialization
	void Start () {
		mDic.Add (typeof(TestDummyClass), this.GetComponent<TestDummyClass>());

		//
		Debug.Log ("----------------------------------");
		Debug.Log ("--------------TEST BEGIN----------");
		int begin1 = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
		Debug.Log ("Hash And Do Begin:" + begin1);
		// Do
		for (int i = 0; i < RepeatCount; i++) {
			if (mDic.ContainsKey(typeof(TestDummyClass))) {
				TestDummyClass tdc = (TestDummyClass)mDic[typeof(TestDummyClass)];
				tdc.DummyFunction();
			}
        }
        
        
		int end1 = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
        Debug.Log ("Hash And Do END:" + end1 + " Cost:" + (end1 - begin1));
		
		Debug.Log ("----------------------------------");
		Debug.Log ("--------------SEPERATOR-----------");
		Debug.Log ("----------------------------------");
		int begin2 = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
		Debug.Log ("GetComponent Do Begin:" + begin2);
		// Do
		for (int i = 0; i < RepeatCount; i++) {
			this.GetComponent<TestDummyClass>().DummyFunction();
		}

		int end2 = DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
		Debug.Log ("GetComponent Do END:" + end2 + " Cost:" + (end2 - begin2));
        Debug.Log ("--------------TEST END------------");
		Debug.Log ("----------------------------------");
		
	}
	
	// Update is called once per frame
	void Update () {
		if (img != null) {
			img.color = color;
		}
	}
}
