using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_System : MonoBehaviour
{

	public static int score_value;
	// staic so global varibale to keep track of the score

	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject); // do not destroy score vaue across screen 
	}

	// Use this for initialization
	void Start ()
	{
	  score_value = 0; // setting it to 0 at the beginning of the game (done in Green Countryside level)
	}
	

}
