using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Uniduino;
public class Over_Win : MonoBehaviour
{

	public Image over;
	// game over image
	public Image wn;
	// win image
	public Image score;
	// display score image
	public Image x_over;
	// image x joypad when losing
	public Image x_win;
	// image x joypad when winning
	public Text st_over;
	// text game over
	public Text st_again;
	// text to start again
	public Text win_win;
	// text congratulation
	public Text win_start;
	// win text to go to next level
	public Text sc_value;
	// text to display the current score
	public Text final;
	// text on the last winning level scene
	public Canvas pause;
	// getting canvas for the pause menu
	public Text over_better;
	// text to display when state is gameover
	public Text newHigh;
	// display new highscore when is beaten
	public GameObject noHigh;
	// display no highscore message  when is not beaten
	public GameObject equalHigh;
	// display message almost beaten highscore 
	public  Text activate;
	// text used to play the activate text animation 
	public Image arrow;
	// image wich show an arrow pointing to the player direction 
	private Player_GameOver pl_over;
	// getting variables from another script
	private Player_Move plm;
	// getting variables from another script
	// audio for falling in water
	public Arduino arduino5;
	static int pinPump = 10; // water pump pin
	float savedTime =0; // store time for the water pump 
	bool stored = true; // store time only once for water pump 

	// Use this for initialization
	void Start ()
	{
		arduino5 = Arduino.global; // arduino initialisation 
		arduino5.Setup (ConfigurePins); // arduino pin configuration 
		pl_over = GameObject.Find ("Player").GetComponent<Player_GameOver> (); // getting Player_GameOver script from player 
		plm = GameObject.Find ("Player").GetComponent<Player_Move> (); // getting Player_GameOver script from player 
		noHigh = GameObject.Find("nohigh"); // find the object 
		equalHigh = GameObject.Find("equal"); // find the object 
		pause.GetComponent<Canvas> (); // getting the pause canvas component 
		pause.enabled = false; // not pause menu visible 
		activate.enabled = false; // no activate text is visible 
		arrow.canvasRenderer.SetAlpha (1.0f);
		// no images are visible 
		over.canvasRenderer.SetAlpha (0.0f);
		st_again.canvasRenderer.SetAlpha (0.0f);
		st_over.canvasRenderer.SetAlpha (0.0f);
		sc_value.canvasRenderer.SetAlpha (0.0f);
		x_win.canvasRenderer.SetAlpha (0.0f);
		x_over.canvasRenderer.SetAlpha (0.0f);
		// no text is visible 
		wn.canvasRenderer.SetAlpha (0.0f);
		score.canvasRenderer.SetAlpha (0.0f);
		win_win.canvasRenderer.SetAlpha (0.0f);
		win_start.canvasRenderer.SetAlpha (0.0f);
		final.canvasRenderer.SetAlpha (0.0f);
		over_better.canvasRenderer.SetAlpha (0.0f);
		newHigh.canvasRenderer.SetAlpha (0.0f);
		noHigh.GetComponent<Text> ().canvasRenderer.SetAlpha (0.0f);
		equalHigh.GetComponent<Text> ().canvasRenderer.SetAlpha (0.0f);
	    
	}

	public void ConfigurePins ()
	{
		arduino5.pinMode (pinPump, PinMode.OUTPUT); // set the pin to input 
		arduino5.digitalWrite(pinPump,Arduino.LOW);
	}

	
	// Update is called once per frame
	void Update ()
	{
		
		activeOver (); // function call 
		activeWin (); // function call 
		activeInWater();
		pause.enabled = pl_over.isPaused; // setting pause canvas visible to a script variable of the player
		spinArrow();// call function to control arrow image 
	}



	void activeOver ()
	{
		if (pl_over.gameOver && pl_over.win == false) { // if gameover and not win 
			checkHighScoreLose (); // check if highscore is beaten function call 
			// fade in all text and gameover screen 
			wn.CrossFadeAlpha (1.0f, 1f, false);
			score.CrossFadeAlpha (1.0f, 1f, false);
			over.CrossFadeAlpha (1.0f, 1f, false);
			st_again.CrossFadeAlpha (1.0f, 1f, false);
			st_over.CrossFadeAlpha (1.0f, 1f, false);
			x_over.CrossFadeAlpha (1.0f, 1f, false);
			sc_value.text = "X" + " " + Score_System.score_value.ToString (); // display score to screen 
			sc_value.CrossFadeAlpha (1.0f, 1f, false);
			over_better.CrossFadeAlpha (1.9f, 1f, false);
			arrow.CrossFadeAlpha (0f, 1f, false); // fade out the arrow image 
		}
	}

	void activeInWater()
	{
		if (pl_over.inWater && stored) { // if player is in water and we can store the time 
			 savedTime = Time.time; // store time 
			 stored = false; // store time only once 

		}

		if (pl_over.inWater) { // if player is in water 
			
			arduino5.digitalWrite (pinPump, Arduino.HIGH); // use the water pump 
			if (Time.time - savedTime >= 5) { // wait for 5 seconds 
				arduino5.digitalWrite (pinPump, Arduino.LOW); // do not use water pump 



			}
		}

		}




	void activeWin ()
	{
       if (pl_over.win == true && pl_over.gameOver == false) { // if win and not gameover 
			// check if highscore is beaten function call 
			checkHighScoreWin ();
			// fade in all text and win screen 
			wn.CrossFadeAlpha (1.0f, 1f, false);
			score.CrossFadeAlpha (1.0f, 1f, false);
			win_win.CrossFadeAlpha (1.0f, 1f, false);
			win_start.CrossFadeAlpha (1.0f, 1f, false);
			sc_value.text = "X" + " " + Score_System.score_value.ToString ();  // display score to screen 
			sc_value.CrossFadeAlpha (1.0f, 1f, false);
			final.CrossFadeAlpha (1.0f, 1f, false);
			x_win.CrossFadeAlpha (1.0f, 1f, false);
			arrow.CrossFadeAlpha (0f, 1f, false); // fade out the arrow image 
		}
	}

	void checkHighScoreLose () // checking the highscore when the player wins or loses the game 
	{

		if (Score_System.score_value > HighScore.highScore) { // if score is greater than highscore 
			
			noHigh.GetComponent<Text> ().enabled = false; // hide highscore not beaten text 
			newHigh.enabled = true; // display new highscore text 
			newHigh.CrossFadeAlpha (1f, 1f, false); // show if player has new highscore 

		} 

		else if (Score_System.score_value < HighScore.highScore) {

			newHigh.enabled = false;// hide highscore text 
			noHigh.GetComponent<Text> ().enabled = true; // display  highscore not beaten text 
			noHigh.GetComponent<Text> ().CrossFadeAlpha (1f, 1f, false); // show if player has new highscore 

		}
		else if (Score_System.score_value == HighScore.highScore) { // if score is the same as the highscore 

			newHigh.enabled = false; // hide new highscore text 
			noHigh.GetComponent<Text> ().enabled = false;// hide no highscore beaten text 
			equalHigh.GetComponent<Text> ().enabled = true; // enable almost highscore achieved 
			equalHigh.GetComponent<Text> ().CrossFadeAlpha (1f, 1f, false); // show if player has new highscore 

		}

	}

	void checkHighScoreWin ()
	{

		if (Score_System.score_value > HighScore.highScore) { // if score is greater than highscore 
			noHigh.GetComponent<Text> ().enabled = false; // hide no highscore beaten text 
			newHigh.enabled = true; // enable new highscore text 
			newHigh.CrossFadeAlpha (1f, 1f, false); // show if player has new highscore 

			// we are not recording the highscore on a winning state a part from the last game level 

		} else if (Score_System.score_value > HighScore.highScore) {
			newHigh.enabled = false; // hide new highscore text 
			noHigh.GetComponent<Text> ().enabled = true; // show no beaten highscore text 
			noHigh.GetComponent<Text> ().CrossFadeAlpha (1f, 1f, false); // show if player has new highscore 

		}
		else if (Score_System.score_value == HighScore.highScore) {  // if score is the same as the highscore 

			newHigh.enabled = false; // hide new highscore text 
			noHigh.GetComponent<Text> ().enabled = false;// hide no highscore beaten text 
			equalHigh.GetComponent<Text> ().enabled = true; // enable almost highscore achieved 
			equalHigh.GetComponent<Text> ().CrossFadeAlpha (1f,1f, false); // show if player has new highscore 

		}
	}

	void spinArrow()
	{

		float angle = Mathf.Atan2(plm.playerRigid.velocity.x, plm.playerRigid.velocity.z) * Mathf.Rad2Deg; // get arrow rotation based on the player velocity 
		arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -(angle+170+50))); // rotate arrow to point player direction 
	}




		
}
