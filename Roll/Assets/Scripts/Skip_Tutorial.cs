using System.Collections;
using Uniduino;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Skip_Tutorial : MonoBehaviour
{
	public Arduino arduino1;
	// arduino object
	private int pinIn = 2;
	// pin to read
	private int selected = 0;
	// assign pin value to this variable


	// Use this for initialization
	void Start ()
	{
		arduino1 = Arduino.global; // setting up ardunio 
		arduino1.Setup (ConfigurePins); // pins configuration 
	}

	public void ConfigurePins ()
	{
		arduino1.pinMode (pinIn, PinMode.INPUT); // setting pin to input 
		arduino1.reportDigital ((byte)(pinIn / 8), 1); // general arduino setup

	}

	// Update is called once per frame
	void Update ()
	{
	
		selected = arduino1.digitalRead (pinIn); // selected = value read from arduino pin 
		Debug.Log ("selected" + selected.ToString ()); // little check if works 

		if (selected == 1 || Input.GetButton ("Skip")) { // if either b or the push button pressed 
			{
				SceneManager.LoadScene ("Transition_Grass"); // load first level transition scene 

			}
		}
	}
}
