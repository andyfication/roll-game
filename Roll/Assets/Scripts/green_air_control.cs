using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Uniduino;

public class green_air_control : MonoBehaviour {

	public Arduino arduino; // arduino object 

	private  int pinRelay1 = 8; // pin that goes to the first relay
	private int  pinRelay2 = 11; // pin that goes to the second relay

	public GameObject[] fires; // array of fires object 

	void Start( )
	{
		arduino = Arduino.global; // initialising arduino 
		arduino.Setup(ConfigurePins); // pin configuration 
		arduino.digitalWrite(pinRelay1,Arduino.HIGH); // no air flowing 
		arduino.digitalWrite(pinRelay2,Arduino.HIGH); // no air flowing 
	
	}

	void Update()
	{

		airCheck (); // call to function to check the air status 
	}

	void ConfigurePins( ) // setting relay pins to output mode 
	{

		arduino.pinMode (pinRelay1, PinMode.OUTPUT);
		arduino.pinMode (pinRelay2, PinMode.OUTPUT);
	}


	void airCheck ()
	{
		arduino.digitalWrite(pinRelay1,Arduino.HIGH); // no air flowing 
		arduino.digitalWrite(pinRelay2,Arduino.HIGH); // no air flowing 

		for(int i = 0;i<fires.Length;i++) // looping the array 
		{
			float distance = Vector3.Distance (fires[i].transform.position, transform.position); // checking the distance between player and all array objects 

			if (distance < 2) // if the distance is less than 2
			{
				arduino.digitalWrite(pinRelay2,Arduino.LOW); // hot air is blowing
			}

		}
	}
}

