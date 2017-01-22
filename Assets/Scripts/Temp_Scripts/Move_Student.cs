using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Student : MonoBehaviour
{

    //Gets the rigidbody of the student as to add force to it
    public Rigidbody2D Student_RigidBody;
    //float that sets the movement speed of the student
    public float Student_Speed;
    //float that keeps track of the specific letter associated to the student 
    public string Student_Letter;
    //keeps tracks of the number of clicks each player has in regards to the specific student
    public int p1_score, p2_score;
    //checks how many waves area the specific student has passed
    public int Exit_Counter;
    //Enums for the direction left and right
    public enum Direction { Left, Right };
    //The movement direction for this particular student.
    public Direction moveDirection;
    Game_Manager_Aleksei gameManager;
	public GameObject mouse;
	public GameObject letterPivot;

    //Variables for floating the text up
    private bool floatText = false;
    private float verticalSpeed = 0.5f;


    // Use this for initialization
    void Start()
    {
        //Get the gameManager
        gameManager = GameObject.Find("GameManager").GetComponent<Game_Manager_Aleksei>();
        //Set the exit counter to 0 as they have not passed any wave area yet
        Exit_Counter = 0;
        //Get the rigid body of the student
        Student_RigidBody = GetComponent<Rigidbody2D>();
        //The specific letter associated to the student is being set in the Inspector
        //Student_Letter = "m";
    }

    // Update is called once per frame
    void Update()
    {
        //Student_RigidBody.AddForce (-transform.right * Student_Speed * Time.deltaTime);
        //Move the student according to the moveDirection enum.
        if (moveDirection == Direction.Left)
        {
            transform.position += new Vector3(-Student_Speed, 0, 0) * Time.deltaTime;
        }
        else if (moveDirection == Direction.Right)
        {
			if (!mouse.GetComponent<SpriteRenderer> ().flipX) {
				mouse.GetComponent<SpriteRenderer> ().flipX = true;
				letterPivot.transform.position += new Vector3(0.20f, 0, 0);
			}
            transform.position += new Vector3(Student_Speed, 0, 0) * Time.deltaTime;
        }

        if (floatText)
        {
            transform.FindChild("LetterPivot").FindChild("Letter").transform.position += new Vector3(0, verticalSpeed, 0) * Time.deltaTime;
            transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<TextMesh>().color -= new Color(0, 0, 0, 1f) * Time.deltaTime;
        }

    }

    /// <summary>
    /// Increases the score. When the player presses the correct key as the student walks past their wave area, then
    /// their score for that specific student increases.
    /// </summary>
    /// <param name="player_score">Player score.</param>
    /// <param name="which_student">Which student.</param>
    public void increase_Score(int player_score, int which_student)
    {

        //checks which wave area/ which player we need to add the points to 
        if (which_student == 1)
        {
            //adds the points to player 1
            p1_score += player_score;
        }
        else if (which_student == 2)
        {
            //adds the points to player 2
            p2_score += player_score;
        }

    }
    void OnTriggerEnter2D(Collider2D wave)
    {
        //checks which wave area the student has collided with
        if (wave.tag == "Player1")
        {
            transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<TextMesh>().color = Color.red;
            //Debug.Log ("Collided with p1");
            //checks that the right key is being pressed
            if (Input.GetKeyDown(Student_Letter))
            {
                //Debug.Log ("The correct key is getting pressed");
                //we increase the score by one;
                increase_Score(1, 1);
            }
            //The same thing is done here, but for player 2.
        }
        else if (wave.tag == "Player2")
        {
            transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<TextMesh>().color = Color.blue;
            if (Input.GetKeyDown(Student_Letter))
            {
                //Debug.Log ("The correct key is getting pressed");
                increase_Score(1, 2);
            }
		} 
        else if (wave.tag == "DeathBox")
        {
            Destroy(gameObject);
        }

    }
    //The same thing is done here but rather then being when the student initially collides with the wave area
    //it checks while the student is still within the wave area.
    void OnTriggerStay2D(Collider2D wave)
    {
        if (wave.tag == "Player1")
        {
            //Debug.Log ("Collided with p1");
            if (Input.GetKeyDown(Student_Letter))
            {
                //Debug.Log ("The correct key is getting pressed");
                increase_Score(1, 1);
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<Display_Letter>().ShakeLetter();
            }
        }
        else if (wave.tag == "Player2")
        {
            if (Input.GetKeyDown(Student_Letter))
            {
                //Debug.Log ("The correct key is getting pressed");
                increase_Score(1, 2);
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<Display_Letter>().ShakeLetter();
            }
        }
        
    }

    //Once the player exits the wave area
    void OnTriggerExit2D(Collider2D wave)
    {
        //We indicate that this specific student has exited the wave area (we keep track of that)
        Exit_Counter += 1;
        //if the wave counter is >=2 then that means it has passed both players and we can now compare their points
        if (Exit_Counter >= 2)
        {
            //This is where we would call the game manager script and add the appropriate points to the winning player
            //if p1 has a higher score then p2 etc...
            if (p1_score > p2_score)
            {
                Debug.Log("P1 had the highest score of: " + p1_score);
                gameManager.IncreasePlayerScore(1, 50);
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<Display_Letter>().student_letter.text = "+50";
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<TextMesh>().color = Color.red;
                FloatText();
            }
            else if (p2_score > p1_score)
            {
                Debug.Log("P2 had the highest score of: " + p2_score);
                gameManager.IncreasePlayerScore(2, 50);
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<Display_Letter>().student_letter.text = "+50";
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<TextMesh>().color = Color.blue;
                FloatText();
            }
            else {
                //if they get the same number of clicks then nothing happens
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<Display_Letter>().student_letter.text = "0";
                transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<TextMesh>().color = Color.black;
                FloatText();
            }
            //Debug.Log ("This is the score for P1: " + p1_score);
           
        }
        else
        {
            transform.FindChild("LetterPivot").FindChild("Letter").GetComponent<TextMesh>().color = Color.black;
        }
       
    }

	void OnCollisionEnter2D(Collision2D student){
		//if student bumps into an other student then the collisions will be ignored.
		if (student.gameObject.tag == "student") {
            Physics2D.IgnoreCollision(student.gameObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
            /*
            else
            {
                Debug.Log("alt fixing " + transform.name + " and " + student.gameObject.name);
                sprite.sortingOrder = 1;
                otherSprite.sortingOrder = 0;
            }*/

        }
	}

    void FloatText()
    {
        floatText = true;
    }

    void OnDestroy()
    {
        gameManager.ReturnLetter(Student_Letter);
        Destroy(gameObject);
    }

}
