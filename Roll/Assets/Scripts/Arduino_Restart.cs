using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

#if (UNITY_3_0 || UNITY_3_0_0 || UNITY_3_1 || UNITY_3_2 || UNITY_3_3 || UNITY_3_4 || UNITY_3_5)		
public class Servo : Uniduino.Examples.Servo { } // for unity 3.x
#endif


public class Arduino_Restart : MonoBehaviour {

	public Arduino arduino; // arduino object 

	private  int pin = 9; // pin to wite to the servo motor
	private int  pinWater = 10; // pin tht control the water pump

	void Start( )
	{
		arduino = Arduino.global; // initialising arduino 
		arduino.Setup(ConfigurePins); // pin configuration 
		arduino.analogWrite(pin, 0); // set servo to 0 degrees angle
		arduino.digitalWrite(pinWater,Arduino.LOW);
		Screen.lockCursor = false;

	}

	void ConfigurePins( ) // setting servo pin to servo mode
	{
		arduino.pinMode(pin, PinMode.SERVO);
		arduino.pinMode (pinWater, PinMode.OUTPUT);
	}
}
