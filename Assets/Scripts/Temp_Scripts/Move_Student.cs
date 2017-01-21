using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Student : MonoBehaviour {
    public Rigidbody2D Student_RigidBody;
    public float Student_Speed;
    public string Student_Letter;
    public int p1_score, p2_score;
    public int Exit_Counter;
    public enum Direction {Left, Right};
    public Direction moveDirection;
	// Use this for initialization
	void Start () {
		Exit_Counter = 0;
		Student_RigidBody = GetComponent<Rigidbody2D> ();
		//Student_Letter = "m";
	}

	// Update is called once per frame
	void Update () {
		//Student_RigidBody.AddForce (-transform.right * Student_Speed * Time.deltaTime);
        if(moveDirection == Direction.Left)
        {
            transform.position += new Vector3(Student_Speed, 0, 0);
        }
        else if (moveDirection == Direction.Right)
        {
            transform.position += new Vector3(-Student_Speed, 0, 0);
        }
		
	}
	public void increase_Score(int player_score, int which_student){
		Debug.Log ("Increase score is being accessed");
		if (which_student == 1) {
			p1_score += player_score;
		} else if (which_student == 2) {
			p2_score += player_score;
		}

	}
	void OnTriggerEnter2D(Collider2D wave){
		if (wave.tag == "Player1") {
			Debug.Log ("Collided with p1");
			if(Input.GetKeyDown(Student_Letter)){
				increase_Score (1, 1);
			}
		}

	}
	void OnTriggerExit2D(Collider2D wave){
		Exit_Counter += 1;
		if (Exit_Counter >= 2) {
			Debug.Log ("This is the score for P1: " + p1_score);
		}
	}

}
