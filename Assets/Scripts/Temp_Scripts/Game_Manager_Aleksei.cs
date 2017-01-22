using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game_Manager_Aleksei : MonoBehaviour {

	public int Player1_Score;
	public int Player2_Score;
    //List of valid Letters.
    public List<string> ValidLetters;
    //List of GameObject students.
    public List<GameObject> Students;
    //List of Valid Y Positions.
    //public List<float> ValidYPositions;

    //Time between each student spawn
    public float waitTime = 2.0f;

    //Min and Max Y Values
    public float minY, maxY;

    //The two x possible spawn positions.
    private Vector3 leftPosition = new Vector3(-10, 0, 0);
    private Vector3 rightPosition = new Vector3(10, 0, 0);

    //Coroutine to spawn students
    private IEnumerator coroutine;
    private bool SpawningStudent = true;

    int leftOrRight = 1;

    public Text playerOneScoreText, playerTwoScoreText;

	// Use this for initialization

	public float Time_Left;
    private float Time_Total;

    public bool freeze = false;

    private int listMax;

    //ScoreCard reveal variables
    public GameObject ScoreCard;
    private bool revealingScore = false;
    private int childIndex = 0;
    private float revealTimer = 0;
    public float revealIntervals = 0.2f;

	void Start () {
        InitValidLetters();
        StartCoroutine(SpawnStudent());
        Time_Total = Time_Left;
        listMax = ValidLetters.Count;
    }
	
	// Update is called once per frame
	void Update () {
        if(Time_Left >= 0)
        {
            Time_Left -= Time.deltaTime;
        }
		
		//Debug.Log (Time_Left);
		if (Time_Left <= 0) {
            freeze = true;
            SpawningStudent = false;
            if(ValidLetters.Count == listMax)
            {
                RevealScoreCard();
            }
		}
        UpdateClockRotation();
        playerOneScoreText.text = Player1_Score.ToString();
        playerTwoScoreText.text = Player2_Score.ToString();
        if (revealingScore)
        {
            if(childIndex < ScoreCard.transform.childCount && revealTimer >= revealIntervals)
            {
                ScoreCard.transform.GetChild(childIndex).gameObject.SetActive(true);
                childIndex++;
                revealTimer = 0;
            }
            else
            {
                revealTimer += Time.deltaTime;
            }
        }
	}

    //Coroutine for spawning students and all relevant operations.
    private IEnumerator SpawnStudent()
    {
        while (SpawningStudent)
        {
            if(ValidLetters.Count > 0)
            {
                //Pick left or right side to spawn.
                leftOrRight = -leftOrRight;
                Vector3 SpawnPosition;
                Move_Student.Direction direction;
                //Set the SpawnPosition to left or right.
                if (leftOrRight == -1)
                {
                    SpawnPosition = leftPosition;
                    direction = Move_Student.Direction.Right;
                }
                else
                {
                    SpawnPosition = rightPosition;
                    direction = Move_Student.Direction.Left;
                }

                //Pick one of the valid heights and add to the y of SpawnPosition
                /*int randomYValue = Random.Range(0, ValidYPositions.Count);
                SpawnPosition.y += ValidYPositions[randomYValue];*/
                SpawnPosition.y = Random.Range(minY, maxY);
                //SpawnPosition.y = (SpawnPosition.y - (SpawnPosition.y % 0.4f)) * 0.4f;
                SpawnPosition.z = SpawnPosition.y;

                //Pick a random student model and spawn at position.
                int index = Random.Range(0, Students.Count);
                GameObject temp = Instantiate(Students[index], SpawnPosition, Quaternion.Euler(new Vector3(-15, 0, 0)));

                //Set the move direction on the spawned student to the correct one.
                temp.GetComponent<Move_Student>().moveDirection = direction;
                temp.GetComponent<Move_Student>().Student_Letter = GetRandomLetter();
            }
            yield return new WaitForSeconds(waitTime);
        }
       
    }

    //Returns a random letter from the List, and removes it from the List so we won't have duplicates.
    string GetRandomLetter()
    {
        int index = Random.Range(0, ValidLetters.Count);
        string letter = ValidLetters[index];
        ValidLetters.Remove(letter);
        return letter;
    }

    //Puts the given letter back into the list.
    public void ReturnLetter(string letter)
    {
        ValidLetters.Add(letter);
    }

    //Just inits the list.
    void InitValidLetters()
    {
        string[] array = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        for(int i = 0; i < array.Length; i++)
        {
            ValidLetters.Add(array[i]);
        }
    }

    float GetRotation()
    {
        //Calculate the target rotation by getting the percentage of the round completed and multiplying by 360.
        float TimePercentage = Time_Left / Time_Total;
        float targetRotation = TimePercentage * 360;
        return targetRotation;
    }

    void UpdateClockRotation()
    {
        GameObject minuteHand = GameObject.Find("minute_hand");
        minuteHand.transform.rotation = Quaternion.Euler(0, 0, GetRotation());
        
    }

    public void IncreasePlayerScore(int player, int scoreValue)
    {
        if(player == 1)
        {
            Player1_Score += scoreValue;
        }
        else if(player == 2)
        {
            Player2_Score += scoreValue;
        }
    }

    //Code to set the ScoreCard string values, and enabled the reveal.
    void RevealScoreCard()
    {
        ScoreCard.SetActive(true);
        revealingScore = true;
        ScoreCard.transform.FindChild("Player1Score").GetComponent<TextMesh>().text = Player1_Score.ToString();
        ScoreCard.transform.FindChild("Player2Score").GetComponent<TextMesh>().text = Player2_Score.ToString();

        //Detect who wins and set the text.
        if (Player1_Score > Player2_Score)
        {
            ScoreCard.transform.FindChild("Winner").GetComponent<TextMesh>().text = "P L A Y E R 1   W I N S";
        }
        else if(Player2_Score > Player1_Score)
        {
            ScoreCard.transform.FindChild("Winner").GetComponent<TextMesh>().text = "P L A Y E R 2   W I N S";
        }
        else
        {
            ScoreCard.transform.FindChild("Winner").GetComponent<TextMesh>().text = "Y O U   A L L   L O S E";
        }
    }

}
