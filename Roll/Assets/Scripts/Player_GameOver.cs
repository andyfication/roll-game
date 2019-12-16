using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using Uniduino;

public class Player_GameOver : MonoBehaviour
{
	public Arduino arduino;
	// arduino object
	private int pinRead = 2;
	// pin 2 as input for arduino
	private int pinPause = 4;
	// pin 3 as input for arduino
	private  int pin = 9; // pin to wite to the servo motor 
	public bool gameOver;
	// is the game over
	public bool inWater;
	// player in water?
	private int selectedPin = 0;
	private int selectedPause = 0;
	// asign the value read by the pin
	public string load;
	// string for the level to load
	public bool win;
	// is the level completed?
	public bool game_finished;
	// is the game finished and completed ?
	public bool isPaused;
	// are we on pause mode ?
	bool attiva = false;
	// support boolean for pause menu
	public AudioSource lost;
	// audio for losing game
	public AudioSource completed;
	// audio for winning game
	public int lose_distance;
	// max distance the player can fall before gameover is called
	bool playLost;
	// play lost sound control
	bool playWin;
	// play win sound control
	private bool activeServo = false;

	// Use this for initialization
	void Start ()
	{
		arduino = Arduino.global; // initialise arduino 
		arduino.Setup (ConfigurePins); // pin configuration 
		gameOver = false; // not game over 
		inWater = false; // not player in water 
		win = false; // not level completed
		isPaused = false; // game not paused 
		game_finished = false; // game not completed 
		playWin = true; // we can play the sound 
		playLost = true; // we can play the sound 

	}

	public void ConfigurePins ()
	{
		arduino.pinMode (pinRead, PinMode.INPUT); // pin set as input 
		arduino.pinMode (pinPause, PinMode.INPUT); // pin set as input 
		arduino.reportDigital ((byte)(pinRead / 8), 1); // standard arduino initialisation 
		arduino.reportDigital ((byte)(pinPause / 8), 1); // standard arduino initialisation 
		arduino.pinMode(pin, PinMode.SERVO);
	}
	
	// Update is called once per frame
	void Update ()
	{
		
		selectedPin = arduino.digitalRead (pinRead); // selected = value read by arduino 
		selectedPause = arduino.digitalRead (pinPause); // check value read for this arduino pin

		if (activeServo) {
			arduino.analogWrite (pin, 75); // set servo to 90 degrees angle and open the prize box in the physical structure
		} else {
			arduino.analogWrite (pin, 0); // set servo to 90 degrees angle and open the prize box in the physical structure

		}


		if (transform.position.y < lose_distance || inWater == true) { // if fall down platforms or in water 
			if (playLost && !win) { // if we can play the sound lost and we did not win the game 
				lost.Play (); // play lost 
				playLost = false; // play only once 
			}
			if (win == false) { // if the player has not won 
				gameOver = true; // game Over is ativated 
				win = false; // not winning state 
			}

		}


		if (Input.GetButton ("Select") || selectedPin == 1) {// if winning state and either b or push button pressed 
			if (game_finished) {
				SceneManager.LoadScene (load); // load  transition scene 
				if (Score_System.score_value > HighScore.highScore) { // check if we have an highscore 
					HighScore.highScore = Score_System.score_value; // set highscore to current score 

				}
			}

		} 


		if (Input.GetButton ("Select") || selectedPin == 1) { // if winning state and either b or push button pressed 
			if (win) {

				SceneManager.LoadScene (load); // load  transition scene 
				activeServo =false;
			}
		}

	    if (Input.GetButton ("Select") || selectedPin == 1) { // else if game over and either b or push button pressed 
			if (gameOver) {
				SceneManager.LoadScene ("Green_Countryside"); // load main menu scene 

				if (Score_System.score_value > HighScore.highScore) { // if score is greater than highscore
					HighScore.highScore = Score_System.score_value; // record score as highscore 
				}
			}

		}



		pause_game (); // call to pause function 

	}






	


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Over")) { // checking if the player is in water 
			inWater = true; // fell in water 
			win = false; // did not win 

		} else if (other.gameObject.CompareTag ("Win")) { // cheching if the player has reached end of level 
			win = true; // won 
			gameOver = false; // not over 
			inWater = false; // not in water 
			if (playWin) { // if we can play the sound 
				completed.Play (); // play the sound 
				playWin = false; // play only once 
			}
		} else if (other.gameObject.CompareTag ("save_score")) { // checking if player collides with final end level 
			activeServo = true;
			game_finished = true; // game is finished 
			win = true; // won 
			gameOver = false; // not over 
			inWater = false; // not in water
			if (playWin) {// if we can play the sound 
				completed.Play (); // play the sound 
				playWin = false;// play only once 



			}
		}

	}



	void pause_game () // pause game function 
	{
		
		if (Input.GetButtonDown ("Menu") || selectedPause == 1) { // if button up and attiva is true 
			
			attiva = true;
		}
			

		if (Input.GetButtonUp("Menu") || selectedPause == 0) {
			if (attiva) {
//			if (Input.GetButton ("Menu") || selectedPause == 1) {
				isPaused = !isPaused; // switch between paused and not paused 
				attiva = false;
				//}
			
				if (isPaused) { // if paused 
					Time.timeScale = 0; // pause the game (time stops)
				
				} else
					Time.timeScale = 1; // reset normal time 
			
			}
		}




		if (isPaused) {

			if (Input.GetButton ("Select") || selectedPin == 1) {// if puasemode and button is pressed 
			
				SceneManager.LoadScene ("Main_Menu"); // go back to the main menu 
				Score_System.score_value = 0; // reset score 
				Time.timeScale = 1; // reset time correctly and exit the pause mode 
			}
		}


	}

}
