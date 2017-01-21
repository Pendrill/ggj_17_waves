using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Display_Letter : MonoBehaviour {
	//stores the tranform of the student 
	public Transform student;
	//stores the letter to be displayed
	public string display_Letter;
	//acccesse the text mesh for that specific student
	public TextMesh student_letter;
	// Use this for initialization
	void Start () {
		//get the parent of the text mesh aka the specific student
		student = transform.parent;
		//store the text mesh into the variable
		student_letter = GetComponent<TextMesh> ();
		//get the specific letter associated to the student
		display_Letter = student.GetComponent<Move_Student> ().Student_Letter;
	}
	
	// Update is called once per frame
	void Update () {
		//display the student's letter onto the screen
		student_letter.text = display_Letter;
	}
}
