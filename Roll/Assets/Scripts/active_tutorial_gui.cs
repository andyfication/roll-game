using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Uniduino;


public class active_tutorial_gui : MonoBehaviour
{

	public  Text activateT;
	// reference to text for activate gui element
	public Image arrw;
	// image for the arrow
	private bool active_sc;
	// boolean used to animate the text
	private Player_Move plmv;
	// getting variables from another script
	public GameObject end;
	public GameObject tut_lift;
	public float speed;
	private bool lift_up = false;

	public Arduino arduino4;
	// arduino object
	private int pinActive = 7;
	// arduino pin
	public int selectedAction = 0;
	// used to read arduino pin

	// Use this for initialization
	void Start ()
	{
		arduino4 = Arduino.global; // arduino initialisation 
		arduino4.Setup (ConfigurePins); // arduino pin configuration 
		activateT.enabled = false; // not enable at the beginning 
		active_sc = false; // boolean false at start 
		arrw.enabled = true; // enable arrow
		plmv = GameObject.Find ("Player").GetComponent<Player_Move> (); // getting Player_GameOver script from player 

	}

	public void ConfigurePins ()
	{
		arduino4.pinMode (pinActive, PinMode.INPUT); // set the pin to input 
		arduino4.reportDigital ((byte)(pinActive / 8), 1); // general arduino setup 

	}
	// Update is called once per frame
	void Update ()
	{
		selectedAction = arduino4.digitalRead (pinActive); // store the value read in selectedAction
		activateAnimation (); // function call to text animation 
		spinArrw();
		moveLift ();
	}


	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("tut")) { // if enters the tutorial zone with the sparks 

			activateT.enabled = true; // activate it 
			arrw.enabled = false; // disable arrow

		}
	}

	public void OnTriggerExit (Collider other)
	{
		if (other.gameObject.CompareTag ("tut")) { // if exits from tutorial zon with sparks 

			activateT.enabled = false; // disable it 
			arrw.enabled = true; // enable arrow
		}
	}

	public void activateAnimation ()
	{
		int sizeMax = 30; // max scaling text size 
		int sizeMin = 20; // min scaling text size 


		if (activateT.fontSize <= sizeMin) { // animate the text 

			active_sc = false; // switch boolean 
		} 

		if (activateT.fontSize >= sizeMax) { // opposite animation 
			active_sc = true; // switch boolean 

		}


		if (active_sc) { // if active 
			activateT.fontSize--; // scale down 
		} 
		if (active_sc == false) { // if not active
			activateT.fontSize++; // scale up 
		}
	}

	void spinArrw()
	{
	    float angle = Mathf.Atan2(plmv.playerRigid.velocity.x, plmv.playerRigid.velocity.z) * Mathf.Rad2Deg; // get arrow rotation based on the player velocity 
		arrw.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -(angle+170+50))); // rotate arrow to point player direction 
	}

	public void moveLift()
	{
		if (activateT.enabled)
		{
			if (Input.GetButton("Select")||selectedAction == 1){

				lift_up = true;

			}
				}


		if (lift_up)
			tut_lift.transform.position = Vector3.MoveTowards (tut_lift.transform.position, end.transform.localPosition, speed*Time.deltaTime); // move to credit section with camera animation
		




	}
}
