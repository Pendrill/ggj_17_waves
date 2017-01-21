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

    public float maxScale = 1.5f, minScale = 0.5f;
    private Vector3 targetScaleMin, targetScaleMax;

    //Blooming effect variables
    public float bloomingSpeed = 0.02f;
    private bool growing;
    private float step;

    //Shake effect variables
    private bool shake = false;
    private float shakeTimer = 0f, shakeDuration = 0.5f;
    private Vector3 originalPosition;

	// Use this for initialization
	void Start () {
        //get the parent of the text mesh aka the specific student
        student = transform.parent.parent;
		//store the text mesh into the variable
		student_letter = GetComponent<TextMesh> ();
		//get the specific letter associated to the student
		display_Letter = student.GetComponent<Move_Student> ().Student_Letter;

        //Calculating target scaling
        targetScaleMin = minScale * transform.localScale;
        targetScaleMax = maxScale * transform.localScale;

        //Saving original position
        originalPosition = transform.localPosition;
        student_letter.text = display_Letter;
    }
	
	// Update is called once per frame
	void Update () {
        //Never-ending Sequence of growing and shrinking. For blooming.
        if (growing)
        {
            transform.localScale = Vector3.Lerp(targetScaleMin, targetScaleMax, step);
            if(step >= 1.0f)
            {
                growing = false;
                step = 0f;
            }
            else
            {
                step += bloomingSpeed;
            }
        }
        else
        {
            transform.localScale = Vector3.Lerp(targetScaleMax, targetScaleMin, step);
            if (step >= 1.0f)
            {
                growing = true;
                step = 0f;
            }
            else
            {
                step += bloomingSpeed;
            }
        }

        //Shaking effect
        if (shake)
        {
            if(shakeTimer < shakeDuration)
            {
                shakeTimer += Time.deltaTime;
                Vector2 randomValues = Random.insideUnitCircle * 0.1f;
                transform.localPosition = originalPosition +  new Vector3(randomValues.x, randomValues.y, 0);
            }
            else
            {
                shake = false;
                shakeTimer = 0f;
                transform.localPosition = originalPosition;
            }
        }
       
	}

    public void ShakeLetter()
    {
        shake = true;
    }
}
