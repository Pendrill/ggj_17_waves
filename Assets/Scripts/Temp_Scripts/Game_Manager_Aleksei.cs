using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager_Aleksei : MonoBehaviour {
	public int Player1_Score;
	public int Player2_Score;
    public List<string> ValidLetters;
    public List<GameObject> Students;
    public List<float> ValidYPositions;

    private Vector3 leftPosition = new Vector3(-10, 0, 0);
    private Vector3 rightPosition = new Vector3(10, 0, 0);

    private IEnumerator coroutine;
    private bool SpawningStudent = true;
	// Use this for initialization
	void Start () {
        InitValidLetters();
        StartCoroutine(SpawnStudent());
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    private IEnumerator SpawnStudent()
    {
        while (SpawningStudent)
        {
           
            //Pick left or right side to spawn.
            int leftOrRight = Random.Range(0, 2);
            Vector3 SpawnPosition;
            if (leftOrRight == 0)
            {
                SpawnPosition = leftPosition;
            }
            else
            {
                SpawnPosition = rightPosition;
            }

            //Pick one of the valid heights and add to the y of SpawnPosition
            int randomYValue = Random.Range(0, ValidYPositions.Count);
            SpawnPosition.y += ValidYPositions[randomYValue];

            //Pick a random student model and spawn at position.
            int index = Random.Range(0, Students.Count);
            GameObject temp = Instantiate(Students[index], SpawnPosition, Quaternion.identity);

            //Set the move direction on the spawned student to the correct one.
            temp.GetComponent<Move_Student>().moveDirection = (Move_Student.Direction) leftOrRight;
            temp.GetComponent<Move_Student>().Student_Letter = GetRandomLetter();
            yield return new WaitForSeconds(2.0f);
        }
       
    }

    string GetRandomLetter()
    {
        int index = Random.Range(0, ValidLetters.Count);
        string letter = ValidLetters[index];
        ValidLetters.Remove(letter);
        return letter;
    }

    void ReturnLetter(string letter)
    {
        ValidLetters.Add(letter);
    }

    void InitValidLetters()
    {
        string[] array = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"};
        for(int i = 0; i < array.Length; i++)
        {
            ValidLetters.Add(array[i]);
        }
    }
}
