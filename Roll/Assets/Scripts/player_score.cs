using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_score : MonoBehaviour
{

	public Text count_score;
	// text to diplay the score
	public AudioSource coins;
	// audio for score picj up
	// Use this for initialization
	void Start ()
	{
		SetScoreText (); //function call 
	}

	void SetScoreText ()
	{
		count_score.text = "X" + "  " + Score_System.score_value.ToString (); // display a string + the score value 
	}

	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Rolly")) { // if there is a collision with coins 
			{
				other.gameObject.SetActive (false); // destroy coin object 
				coins.Play (); // play the sound 
				Score_System.score_value += 1; // increase the score 
				SetScoreText (); // display score text 

			}
		}

	}
}
