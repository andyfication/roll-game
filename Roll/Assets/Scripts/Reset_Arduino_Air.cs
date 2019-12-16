using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;
public class Reset_Arduino_Air : MonoBehaviour {

	public Arduino arduino; // arduino object 

	private  int pinRelay1 = 8; // pin that goes to the first relay
	private int  pinRelay2 = 11; // pin that goes to the second relay
	private  int pinGreenLevel = 12 ; // pin that goes to the first level led
	private int  pinIcySnow = 13; // pin that goes to the second level led

	void Start( )
	{
		arduino = Arduino.global; // initialising arduino 
		arduino.Setup(ConfigurePins); // pin configuration 

		arduino.digitalWrite(pinRelay1,Arduino.HIGH); // no air flowing 
		arduino.digitalWrite(pinRelay2,Arduino.HIGH); // no air flowing 
		arduino.digitalWrite(pinGreenLevel,Arduino.HIGH); // led off
		arduino.digitalWrite(pinIcySnow,Arduino.LOW); // led off

	}

	void ConfigurePins( ) // setting relay pins to output mode 
	{

		arduino.pinMode (pinRelay1, PinMode.OUTPUT);
		arduino.pinMode (pinRelay2, PinMode.OUTPUT);
		arduino.pinMode (pinGreenLevel, PinMode.OUTPUT);
		arduino.pinMode (pinIcySnow, PinMode.OUTPUT);
	}
}
