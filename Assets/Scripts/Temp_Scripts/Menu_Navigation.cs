using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_Navigation : MonoBehaviour {
	public bool Start_Screen;
	//public bool Game_Scene;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Start_Screen) {
			if (Input.GetKeyDown (KeyCode.Space)) {
				SceneManager.LoadScene ("Aleksei_Test");
			}
		}
	}
}
