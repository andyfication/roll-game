using UnityEngine;
using System.Collections;
using Uniduino;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Button_Colour_Picker : MonoBehaviour
{
	public Arduino arduino1;
	// arduino object
	private int pinIn = 2;
	// input pin
	private int selectedPin = 0;
	// boolean to assign the pin value

	public Image fade_click;
	// white fade effect before going to tutorial


	// Use this for initialization
	void Start ()
	{
		arduino1 = Arduino.global; // initialising arduino
		arduino1.Setup (ConfigurePins); // arduino standard configuration 
		fade_click.canvasRenderer.SetAlpha (0.0f); // no fade active 
	}

	public void ConfigurePins ()
	{
		arduino1.pinMode (pinIn, PinMode.INPUT); // set pinIn in input mode 
		arduino1.reportDigital ((byte)(pinIn / 8), 1); // standard arduino initialisation 

	}

	// Update is called once per frame
	void Update ()
	{

		selectedPin = arduino1.digitalRead (pinIn); // select = the value read by pinIn
		Debug.Log ("selected" + selectedPin.ToString ()); // little console print 

		if (selectedPin == 1 || Input.GetButton ("Select")) { // if push button or keyboard b is pressed 

			{
				fade_click.CrossFadeAlpha (1.0f, 1f, false); // start screen fade in 
				Invoke ("Begin_Tut", 1);// this will happen after 2 seconds ( go to next scene)
			}
		}

		}

	void Begin_Tut()
	{

		SceneManager.LoadScene ("Tutorial"); // load tutorial scene
	}
}

