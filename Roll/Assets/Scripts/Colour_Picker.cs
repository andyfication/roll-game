using UnityEngine;
using System.Collections;
using Uniduino;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Colour_Picker : MonoBehaviour
{
	public Arduino arduino;
	// analog pin
	private int redPin = 5;
	// arduino object
	private int bluePin = 1;
	// analog pinß
	private int greenPin = 3;
	// analog pin


	private int _red;
	// how much red
	private int _green ;
	// how much green
	private int _blue;
	// how much blue

	private int pinOutRed = 3;// red pin output
	private int pinOutGreen = 5; // green pin output
	private int pinOutBlue = 6; // blue pin output






	//private int pinRedWrite = 9;
	public Renderer player;
	// getting the player renderer component


	// Use this for initialization
	void Start ()
	{
		arduino = Arduino.global; //arduino setup 
		arduino.Setup (ConfigurePins); // configuaration arduino pins 
		player = GetComponent<Renderer> (); // get player renderer component 
		_red = 0; // initialise red component
		_green = 0; // initialise green component
		_blue = 0;// initialise blue component
	
	}

	void ConfigurePins ()
	{
		arduino.pinMode (redPin, PinMode.ANALOG); // reading analog pin one
		arduino.reportAnalog (redPin, 1);
		arduino.pinMode (greenPin, PinMode.ANALOG); // reading analog pin zero
		arduino.reportAnalog (greenPin, 1);
		arduino.pinMode (bluePin, PinMode.ANALOG); // reading analog pin two
		arduino.reportAnalog (bluePin, 1);
		arduino.pinMode(pinOutRed, PinMode.PWM); // set pin to pwm mode
		arduino.pinMode(pinOutGreen, PinMode.PWM); // set pin to pwm mode
		arduino.pinMode(pinOutBlue, PinMode.PWM); // set pin to pwm mode

	}
	
	// Update is called once per frame
	void Update ()
	{
		constrain (); // costrain function call
		pwmSignal ();// call to function for pwm trasmission 
		player.material.color = new  Color32 ((byte)_red, (byte)_green, (byte)_blue, 1); // assign new colour to player material 
	}








	void constrain ()
	{
		_red = arduino.analogRead (redPin); // read and assign red value 
		// costarin between 0-255
		if (_red > 255)
			_red = 255;
		else if (_red < 0)
			_red = 0;
		_green = arduino.analogRead (greenPin); // read and assign green value 
		// costarin between 0-255
		if (_green > 255)
			_green = 255;
		else if (_green < 0)
			_green = 0;
		_blue = arduino.analogRead (bluePin); // read and assign blue value 

		// costarin between 0-255
		if (_blue > 255)
			_blue = 255;
		else if (_blue < 0)
			_blue = 0;
	}


	void pwmSignal()
	{
		arduino.analogWrite (pinOutRed, _red); // send pwm for red colour
		arduino.analogWrite (pinOutGreen, _green); // send pwm for green colour
		arduino.analogWrite (pinOutBlue, _blue); // send pwm for blue colour
	}

}
