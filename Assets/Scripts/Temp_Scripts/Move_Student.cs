using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Student : MonoBehaviour {
	public Rigidbody2D Student_RigidBody;
	public float Student_Speed;
	// Use this for initialization
	void Start () {
		Student_Speed = 3;
		Student_RigidBody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		Student_RigidBody.AddForce (-transform.right * Student_Speed * Time.deltaTime);
		//transform.position += new Vector3(-0.1f, 0, 0);
	}


}
