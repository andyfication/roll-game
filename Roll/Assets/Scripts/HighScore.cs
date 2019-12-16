using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HighScore : MonoBehaviour
{

	static public Text display_high;
	// text to display highscore
	public static int highScore;
	// integer to store highscore


	void Awake ()
	{
		DontDestroyOnLoad (this.gameObject); // dont destroy this object across the scenes 
	}

	// Use this for initialization
	void Start ()
	{
		display_high = GameObject.Find ("high").GetComponent<Text> (); // get text component from display_high
	}
	
	// Update is called once per frame
	void Update ()
	{
		displayHighScore (); // call function to display highscore
	}

	void displayHighScore ()
	{
		display_high.text = "Best" + " " + "Score" + " " + ":" + " " + highScore.ToString (); // display highscore text on screen 


	}
}
