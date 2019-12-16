using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Uniduino;

public class Main_Menu : MonoBehaviour
{

	public Button playButton;
	// butoon to start the level
	public Button exitButton;
	// button to exit the game
	public Button creditButton;
	// button to go to credits
	public Button Menu_Back;
	// button used to go back from credits

	public Text play;
	// start the level text
	public Text credit;
	// credits text
	public Text exit;
	// exit game text
	public Text green;
	// green island text
	public Text icy;
	// white island text
	public Text dry;
	// brown island text
	public Text textCredits;
	// text used to display credits
	public Text back_Menu;
	// text used from credits section to go back to main menu

	public Arduino arduino;
	// arduino object
	public Animator camera_start;
	// camera animator when the game starts
	public Image fade_start;
	// white fade effect when game starts

	public Image x;
	// image with the joypad x

	private int pinIn = 2;
	// input pin 2 for select button
	public int buttonPressed;
	// check if 0/1 button pressed
	public string level;
	// stage of the main menu we are in
	public float speed;
	// speed of animations
	public float rotateX;
	// float used to rotate sphere
	public float rotateY;
	// float used to rotate sphere
	public float rotSPeed;
	// speed rotation for sphere

	public Transform target;
	// target for sphere animation
	public Image fade_options;
	// text fade animation
	public Image fade_options1;
	// text fade animation
	public Image fade_options2;
	// text fade animation
	public GameObject c_one;
	// camera object
	public GameObject c_Begin;
	// point of camera begin aniamtion
	public GameObject c_End;
	// point of camera end animation
	public GameObject sphere;
	// sphere object

	bool makeTransition;
	// used to check if mouse or arduino used

	bool makeTransitionArduino;
	// used for arduino in crdits


	// Use this for initialization
	public void Start ()
	{
		arduino = Arduino.global; // initialise arduino 
		arduino.Setup (ConfigurePins); // calling arduino set up function 
		playButton = GetComponent<Button> (); // get button component 
		exitButton = GetComponent<Button> ();// get button component 
		creditButton = GetComponent<Button> ();// get button component 
		Menu_Back = GetComponent<Button> ();// get button component 
		buttonPressed = 0; // no button was pressed (arduino)
		camera_start.GetComponent<Animator> (); // get the animator component 
		camera_start.enabled = false; // animation for camera is not active 
		fade_start.canvasRenderer.SetAlpha (0.0f); // no fade active 
		play.enabled = true; // text active 
		credit.enabled = true;// text active 
		exit.enabled = true;// text active 
		green.enabled = true;// text active 
		dry.enabled = true;// text active 
		icy.enabled = true;// text active 
		rotateX = 0.0f; // no rotation applied to the sphere 
		rotateY = 0.0f;// no rotation applied to the sphere 
		fade_options.canvasRenderer.SetAlpha (1.0f); // cover all menu options at the beginning 
		fade_options1.canvasRenderer.SetAlpha (1.0f);// cover all menu options at the beginning 
		fade_options2.canvasRenderer.SetAlpha (1.0f);// cover all menu options at the beginning 
		back_Menu.enabled = false; // not in credit section yet 
		textCredits.enabled = false; // no credit text displayed 
		makeTransition = false; // no camera transition arduino
		x.enabled = false; // image of joypad x is not ebabked 
		HighScore.display_high.enabled = true; // display highscore 
		makeTransitionArduino = false;//no animation for credits 
	}



	public void ConfigurePins () // arduino pin settings 
	{
		arduino.pinMode (pinIn, PinMode.INPUT);
		arduino.reportDigital ((byte)(pinIn / 8), 1);

	}


	void Update ()
	{
		buttonPressed = arduino.digitalRead (pinIn); // read pin 2 and store in buttonPressed
		Debug.Log (buttonPressed); // quick check if it is working 

		if (buttonPressed == 1 && level == "go") { // we over start level and press button (arduino)
			
			camera_start.enabled = true; // start camera animation 
			fade_start.CrossFadeAlpha (1.0f, 3f, false); // start screen fade in 
			Invoke ("Begin", 4);// this will happen after 2 seconds ( go to next scene)
			play.enabled = false; // text not active 
			credit.enabled = false; // text not active 
			exit.enabled = false; // text not active 
			fade_options.enabled = false; // fade not active 
			fade_options1.enabled = false;// fade not active 
			fade_options2.enabled = false;// fade not active 
			green.enabled = false; // no island text displayed 
			dry.enabled = false;// no island text displayed 
			icy.enabled = false;// no island text displayed 
		}

		if (buttonPressed == 1 && level == "quit") { // if over exit and button is pressed (arduino)
			Application.Quit (); // application quit
			Debug.Log ("quit"); // quick check 
		}
		if ( buttonPressed == 1 && level == "credArduino" ) { // when we click credits with arduino
			makeTransitionArduino = true; // arduino click we play camera animation 
			HighScore.display_high.enabled = false; // dont display highscore text 

		}


		if (level == "cred") { // when we click credits 
			c_one.transform.position = Vector3.MoveTowards (c_one.transform.position, c_End.transform.localPosition, 0.5f); // move to credit section with camera animation
			HighScore.display_high.enabled = false; // dont display highscore
			if (c_one.transform.position == c_End.transform.localPosition) { // when the animation is finished and we reach the target 
				back_Menu.enabled = true; // enable button to go back 
				x.enabled = true; // enable image x joypad 
				textCredits.enabled = true; // enable credits text 
				makeTransition = true; // mouse click and finished camera animation makeTransition is ready to be played again
			}

		}


	

		if (makeTransition||makeTransitionArduino) { // if animation then do following 
			c_one.transform.position = Vector3.MoveTowards (c_one.transform.position, c_End.transform.localPosition, 0.5f); // move to credit section with camera animation
			// disable all main menu text and components 
			play.enabled = false;
			credit.enabled = false;
			exit.enabled = false;
			fade_options.enabled = false;
			fade_options1.enabled = false;
			fade_options2.enabled = false;
			green.enabled = false;
			dry.enabled = false;
			icy.enabled = false;


			if (c_one.transform.position == c_End.transform.localPosition) { // when the animation is finished and we reach the target 
				back_Menu.enabled = true; // enable button to go back 
				x.enabled = true; // enable image x joypad 
				textCredits.enabled = true; // enable credits text 
				makeTransition = true; // mouse click and finished camera animation makeTransition is ready to be played again
				makeTransitionArduino = false; // button push and camera animation ready to be played again 
			}
		}


		if (level == "menu") { // if we go back from credits to main menu 
			
			c_one.transform.position = Vector3.MoveTowards (c_one.transform.position, c_Begin.transform.localPosition, 0.5f); // move to menu section with camera animation 
			back_Menu.enabled = false; // disable credit button 
			x.enabled = false; // disale x joypad image
			textCredits.enabled = false; // disable credit text 
			if (c_one.transform.position == c_Begin.transform.localPosition) { // when we reach the camera target to main menu 
				play.enabled = true; // activate text 
				credit.enabled = true; // activate text 
				exit.enabled = true; // activate text 
				fade_options.enabled = true;// activate text 
				fade_options1.enabled = true;// activate text 
				fade_options2.enabled = true;// activate text 
				green.enabled = true; // activate text 
				dry.enabled = true; // activate text 
				icy.enabled = true; // activate text 
				HighScore.display_high.enabled = true; // enable highscore text 

			}
			makeTransition = false; // no animation needed or opposite camera animation 
			makeTransitionArduino=false; // no animation needed for push button arduino 
		}

		if (buttonPressed == 1 || Input.GetButton ("Select") && level == "menuArduino") { // if we go back from credits to main menu with arduino

			makeTransition = false; // no animation or opposite one needed 
			x.enabled = false; // diable x image joypad 

		}
		if (!makeTransition && level == "menuArduino") { // if !makeTransition we go back to th main menu with the camera 

			c_one.transform.position = Vector3.MoveTowards (c_one.transform.position, c_Begin.transform.localPosition, 0.5f); // move to menu section with camera animation 
			back_Menu.enabled = false; // disable credit button 
			textCredits.enabled = false; // disable credit text 
			if (c_one.transform.position == c_Begin.transform.localPosition) { // when we reach the camera target to main menu 
				play.enabled = true; // activate text 
				credit.enabled = true; // activate text 
				exit.enabled = true; // activate text 
				fade_options.enabled = true;// activate text 
				fade_options1.enabled = true;// activate text 
				fade_options2.enabled = true;// activate text 
				green.enabled = true; // activate text 
				dry.enabled = true; // activate text 
				icy.enabled = true; // activate text 
				HighScore.display_high.enabled = true; // enable highscore text 

			}
		}



		float speed_1 = speed * Time.deltaTime; // temporary speed used for sphere transition/animation 
		sphere.transform.position = Vector3.MoveTowards (sphere.transform.position, target.position, speed_1); // start animation 
		sphere.transform.Rotate (new Vector3 (+rotateX, 0, -rotateY)); // apply rotation to sphere 
		rotateX += rotSPeed; // increment rotation 
		rotateY += rotSPeed; // increment rotation 

		if (sphere.transform.position == target.position) { // when we reach the tartget 
			rotateX = 0.0f; // stop rotate 
			rotateY = 0.0f;// stop rotate 
			fade_options.CrossFadeAlpha (0.0f, 3f, false); // enable menu options 
			fade_options1.CrossFadeAlpha (0.0f, 3f, false);// enable menu options 
			fade_options2.CrossFadeAlpha (0.0f, 3f, false);// enable menu options 
		}
	}

	public void Begin () // start function 
	{

		SceneManager.LoadScene ("Colour_Pick"); // load next game scene 
	}


	public void startGame () // start option (arduino version)
	{
		if (level != "cred") { // if not credit than start 
			level = "go";
		}

	}

	public void startGameMous () // start option (mouse version)
	{
		camera_start.enabled = true; // animate camera 
		fade_start.CrossFadeAlpha (1.0f, 3f, false); // start fade in 
		Invoke ("Begin", 4);//this will happen after 2 seconds

		// disable all text components 
		play.enabled = false;
		credit.enabled = false;
		exit.enabled = false;
		fade_options.enabled = false;
		fade_options1.enabled = false;
		fade_options2.enabled = false;
		green.enabled = false;
		dry.enabled = false;
		icy.enabled = false;

	}

	public void creditGame () // credit ption (mouse version)
	{
		level = "cred"; // go to credit stage 

		// disable all main menu text and components 
		play.enabled = false;
		credit.enabled = false;
		exit.enabled = false;
		fade_options.enabled = false;
		fade_options1.enabled = false;
		fade_options2.enabled = false;
		green.enabled = false;
		dry.enabled = false;
		icy.enabled = false;

	}

	public void creditGameArduino () // credit ption (arduino version)
	{
		level = "credArduino"; // go to credit stage 

	
	}


	public void exitGame () // quit section (arduino version)
	{
		if (level != "cred") { // if not credits 
			level = "quit"; // quit level 
		}

	}

	public void exitGameMouse () // quit section (mouse version)
	{
		Application.Quit (); // application quit
		Debug.Log ("quit"); // quick check 
	}


	public void backToMenu () // back to menu (mouse version)
	{
		
		level = "menu"; // go to mouse section 


	}

	public void backToMenuArduino () // back to menu (arduino version)
	{

		level = "menuArduino"; // go to mouse section 


	}

}
