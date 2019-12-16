using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;
public class zero_level_leds : MonoBehaviour {

	public Arduino arduino; // arduino object 

	private  int pinGreenLevel = 12 ; // pin that goes to the first level led
	private int  pinIcySnow = 13; // pin that goes to the second level led



	void Start( )
	{
		arduino = Arduino.global; // initialising arduino 
		arduino.Setup(ConfigurePins); // pin configuration 
		arduino.digitalWrite(pinGreenLevel,Arduino.LOW); // led off
		arduino.digitalWrite(pinIcySnow,Arduino.LOW); // led off
	}


	void ConfigurePins( ) // setting led pins to output mode 
	{

		arduino.pinMode (pinGreenLevel, PinMode.OUTPUT);
		arduino.pinMode (pinIcySnow, PinMode.OUTPUT);
	}
}
