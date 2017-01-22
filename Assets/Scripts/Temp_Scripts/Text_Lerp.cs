using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Text_Lerp : MonoBehaviour {
	public GameObject Start_Text;
	public GameObject Mice;
    public float t, q, g;
	public Vector3 Original_Size, Original_Position;
    public bool growing, growing2;
	// Use this for initialization
	void Start () {
		t = 0f;
        g = 0f;
		Original_Size = Start_Text.transform.localScale;
		Original_Position = Mice.transform.position;
		growing = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (growing) {
			t += Time.deltaTime / 2;
		} else {
			t -= Time.deltaTime / 2;
		}

        if (growing2)
        {
            q += Time.deltaTime;
        }
        else
        {
            q -= Time.deltaTime;
        }

		Mice.transform.position = Vector3.Lerp (Original_Position, Original_Position + new Vector3(0,0.2f,0), t);
		Start_Text.transform.localScale = Vector3.Lerp(Original_Size, new Vector3(1.5f,1.5f,1.5f), q );
		Start_Text.transform.position = Vector3.Lerp(new Vector3(0.5f, -1.5f, 0f) , new Vector3(-0.5f,-0.5f,0f),t );
		Start_Text.transform.rotation = Quaternion.Lerp (Quaternion.Euler(new Vector3 (0, 0, -15)), Quaternion.Euler(new Vector3 (0, 0, 15)), t);
		Start_Text.GetComponent<Renderer> ().material.color = Color.Lerp (Color.red, Color.blue, t);
		if (t > 1) {
			growing = false;
		}else if ( t< 0){
			growing = true;
		}

        if ( q > 1)
        {
            growing2 = false;
        }
        else if ( q < 0)
        {
            growing2 = true;
        }

	}
}
