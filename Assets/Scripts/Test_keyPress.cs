using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_keyPress : MonoBehaviour {
	public string letter;
	// Use this for initialization
	void Start () {
		letter = "m";
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (letter)) {
			Debug.Log ("key was pressed");
		}
	}
}
