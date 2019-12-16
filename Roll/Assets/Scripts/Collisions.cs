using UnityEngine;
using System.Collections;
using Uniduino;
using UnityEngine.UI;

public class Collisions : MonoBehaviour
{
	public bool lift_Collision;
	// collision with the lift
	private bool activeLift;
	// is the lift active
	public bool key_collision;
	// key collected
	public bool catOn;
	// is the catapult on
	public AudioSource elev;
	// audio elevator
	public AudioSource ski_lift;
	// audio for lift_ski
	private bool playSkilift;
	// used to check sound for skilift
	public AudioSource keyS;
	// audio for the key
	public AudioSource water;
	// audio for falling in water
	public Arduino arduino4;
	// arduino object
	public int pinActive = 7;
	// arduino pin
	public int selectedAction = 0;
	// used to read arduino pin
	public AudioSource cart_mine_sound;
	// sound used for minecart
	public AudioSource cart_collision_sound;
	// sound used for minecart collision
	public AudioSource cannon_fire_sound;
	// sound used fir cannon fire
	public AudioSource destroy_woods;
	// sound used for wood boxes
	private bool destr_wood = true;
	// used to play the boxes wood sound
	public bool snowLiftReadyGoingUp;
	// snow lift entering lift
	public bool snowLiftGoingUp;
	// snow lift going up
	public bool snowLiftGoingDown;
	// snow lift going down
	private bool canPlayel = true;
	//bool used to play evelator sound onlny once
	private bool passageSound = true;
	//bool used to play passage sound onlny once
	public GameObject snowLift;
	// actual snow lift
	public GameObject snowStart;
	// snow target lift
	public GameObject snowMiddle;
	// snow middle  target lift
	public GameObject snowTop;
	// snow top target lift


	public bool west_lift_up;
	// boolean used in west level for lift activation
	public bool west_lift_moving;
	// west lift is moving up?


	private GameObject target;
	// target for the snow lift to store the current one


	public bool mineCartMoving;
	// is west minecart ready to move?
	public bool mineCartReady;
	// west minecart is ready to move
	public bool canPlaymine = true;
	// west minecart sound check

	private float passSpeed;
	// float used to control the rotation passage speed in the snow level
	private bool isPassRotating;
	// is the passage rotating?
	private bool rotatePass;
	// start passage rotation when true

	public cannon_arrived can;
	// getting variables from another script
	public ParticleSystem can_explosion;
	// particle system for cannon explosion
	public GameObject water_particle;
	// particle system for barrels falling in water


	private int sizeMax;
	// max size for text active scaling
	private int sizeMin;
	// min size for text active scaling
	private bool active_scaler;
	// is animation for active text on ?
	public Over_Win act;
	// reference to Over_Win script reference
	// Use this for initialization

	void Start ()
	{
		arduino4 = Arduino.global; // arduino initialisation 
		arduino4.Setup (ConfigurePins); // arduino pin configuration 
		lift_Collision = false; // no lift collision 
		activeLift = false; // lift is not active 
		key_collision = false; // not collision with a key 
		catOn = false; // cataput is off 
		snowLiftGoingUp = false; // lift is not moving 
		snowLiftReadyGoingUp = false; // lift is not ready 
		snowLiftGoingDown = false; // lift is going down 
		target = snowMiddle; // current target is snowMiddle 
		passSpeed = 40f; // setting passage speed;
		isPassRotating = false; // passage not active 
		rotatePass = false; // passage not rotating yet
		mineCartMoving = false; // minecart is not moving 
		mineCartReady = false; // minecart is not ready to move
		can = GameObject.Find ("west_cannon").GetComponent<cannon_arrived> (); // getting the cannon arrived script attached to west_cannon 
		act = GameObject.Find ("Menu").GetComponent<Over_Win> (); // getting the Over_Win script attached to Menu
		west_lift_up = false; // set west lift up to not active
		can_explosion.GetComponent<ParticleSystem> (); // getting the particle component from ParticleSystem
		active_scaler = true; // set active animation text to true  
		canPlaymine = true; // we can play this sound 
		playSkilift = true;  // we can play this sound 
		destr_wood = true;  // we can play this sound 


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

		if (selectedAction == 1 || Input.GetButton ("Select")) {// if the lift is active and either b or button is pushed 
			if( activeLift){
			lift_Collision = true; // lift collision 
			elev.Play (); // playe lift sound 
			activeLift = false; // lift is not active 
			}
		}

		snowLiftMovement (); // snow lift call function 

		passageRotation (); // rotation for passage call function 


		mineCartMotion (); // moving mineCart funcyion call

		cannonFire (); // call function to cannon fire 
	
		WestLiftMove (); // call function to lift 
	
		activateAnimation (); // call function to activate text 
	}


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Lift_Cube")) {
			activeLift = true; // activate lift 
			act.activate.enabled = true; // show active text 
			act.arrow.enabled = false; // disable arrow gui
		} else if (other.gameObject.CompareTag ("Lift_Cube_Down")) {
			lift_Collision = false; // lift not active 
			activeLift = false; // lift not active
		} else if (other.gameObject.CompareTag ("Key")) {
			other.gameObject.SetActive (false); // remove key 
			key_collision = true; // collision detected with key 
			keyS.Play (); // play key sound 
		} else if (other.gameObject.CompareTag ("Cat")) {
			catOn = true; // catapult on 
			act.activate.enabled = true;  // show active text 
			act.arrow.enabled = false;// disable arrow gui
		} else if (other.gameObject.CompareTag ("snowLift")) {
			snowLiftReadyGoingUp = true; // snow lift ready to go up
			act.activate.enabled = true;  // show active text 
			act.arrow.enabled = false;// disable arrow gui
		} else if (other.gameObject.CompareTag ("lift_arrival")) {
			snowLiftReadyGoingUp = false;// disable arrow gui
			snowLiftGoingDown = true; // snow lift ready to go down
		} else if (other.gameObject.CompareTag ("passage")) {
			act.activate.enabled = true;  // show active text 
			act.arrow.enabled = false;// disable arrow gui
			isPassRotating = true; // passage is now active
		} else if (other.gameObject.CompareTag ("mineCart")) {
			act.arrow.enabled = false;// disable arrow gui
			act.activate.enabled = true;  // show active text 
			mineCartReady = true; // mineCart is now active
		} else if (other.gameObject.CompareTag ("west_lift")) {
			act.activate.enabled = true;  // show active text 
			act.arrow.enabled = false;// disable arrow gui
			west_lift_up = true; // lift is going up 
		} else if (other.gameObject.CompareTag ("west_lift_arrival")) {
			west_lift_up = false; // lift not going up 
			west_lift_moving = false; // the lift is not moving 
			canPlayel = true; // sound elevator ready to play
		} else if (other.gameObject.CompareTag ("Over")) {
			// intanciate a water particle at player position when fall in water 
			GameObject temp = Instantiate (water_particle, new Vector3 (this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z), Quaternion.identity) as GameObject;
			water.Play (); // play splash sound

		} else if (other.gameObject.CompareTag ("wood_down")) {
			if (destr_wood) { // if we can plya sound 
				destroy_woods.Play (); // play sound of destroy wood on collision 
				destr_wood = false; // play the sound only once 
			}
		} 

	}

	void OnTriggerExit (Collider other) // when exit from each of the above to the following 
	{
		if (other.gameObject.CompareTag ("Lift_Cube")) {
			act.activate.enabled = false; // active text not true 
			act.arrow.enabled = true; // enable arrow gui
		} else if (other.gameObject.CompareTag ("Cat")) {
			catOn = true; // catapult on 
			act.activate.enabled = false; // active text not true 
			act.arrow.enabled = true;// enable arrow gui
		} else if (other.gameObject.CompareTag ("snowLift")) {
			snowLiftReadyGoingUp = true; // snow lift ready to go up
			act.activate.enabled = false;// active text not true 
			act.arrow.enabled = true;// enable arrow gui
		} else if (other.gameObject.CompareTag ("passage")) {
			act.activate.enabled = false;// active text not true 
			act.arrow.enabled = true;// enable arrow gui
			isPassRotating = true; // passage is now active
		} else if (other.gameObject.CompareTag ("mineCart")) {
			mineCartReady = true; // mineCart is now active
			act.activate.enabled = false;// active text not true 
			act.arrow.enabled = true;// enable arrow gui
		} else if (other.gameObject.CompareTag ("west_lift")) {
			west_lift_up = true; // up with the lift 
			act.activate.enabled = false;// active text not true 
			act.arrow.enabled = true;// enable arrow gui
		} 

	}


	public void snowLiftMovement ()
	{
		if (selectedAction == 1 || Input.GetButton ("Select")) { // if snow lift ready to go up and either b or button pressed 
			if (snowLiftReadyGoingUp){
				snowLiftGoingUp = true; // lift going up 
			playSkilift = true; // play lift sound is true
			if (playSkilift) { // if we can play the sound 
				ski_lift.Play (); // play the sound 
				playSkilift = false; // set the play sound to false 
			}
		}
	}

		if (snowLiftGoingUp) { // if going up 

			snowLift.transform.position = Vector3.MoveTowards (snowLift.transform.position, target.transform.position, 2f * Time.deltaTime); // transform to target 
			if (snowLift.transform.position == snowMiddle.transform.position) { // if we reach the first target 
				
				target = snowTop; // change target to another
				}

			if (snowLift.transform.position == snowTop.transform.position) { // if lift is at the top 
				snowLiftGoingUp = false; // dont go up more 
				snowLiftReadyGoingUp = false; // not ready to go up 
				target = snowMiddle; // redy to go back down
				ski_lift.Stop (); // stop playing the lift sound 
			}

		}

		if (snowLiftGoingDown) { // if the lift goes down 
				
			snowLift.transform.position = Vector3.MoveTowards (snowLift.transform.position, target.transform.position, 2f * Time.deltaTime); // transform to middle target 

			if (snowLift.transform.position == snowMiddle.transform.position) { // if we reach the middle target and we go down  

				target = snowStart; // change target to beginning 
			}

			if (snowLift.transform.position == snowStart.transform.position) { // if the lift is at the start 
				snowLiftGoingDown = false; // not ready to go down 
				target = snowMiddle; // target set to be the midlle one 
			}
		}

	}


	public void passageRotation ()
	{
		if (selectedAction == 1 || Input.GetButton ("Select")) { // if either b or push button and passage rotation is active 
			if( isPassRotating)
			{
			if (passageSound) { // if we can play the passage sound 
				elev.Play (); // playe lift sound 
				rotatePass = true; // roatte the object passage 
				passageSound = false; // play the sound only once 
			}
		}
		}
		
		if (rotatePass) {
			GameObject.FindGameObjectWithTag ("passage").transform.Rotate (0f, 0f, -(passSpeed * Time.deltaTime)); // rotate object around the z axis 

		}

	}


	public void mineCartMotion ()
	{
		if (selectedAction == 1 || Input.GetButton ("Select")) { // if either b or push button and passage rotation is active
			if (mineCartReady) {
			
				if (canPlaymine) { // can we play minecart sound ?
					mineCartMoving = true; // move the micecart 
					cart_mine_sound.Play (); // play the sound 
					canPlaymine = false; // play sound only once 
				}

			}
		}

	}


	public void cannonFire ()
	{
		Vector3 shoot = new Vector3 (10, 1, 0); // physics impulse for the cannon shot 
		if (selectedAction == 1 || Input.GetButton ("Select")) {// if either b or push button and passage rotation is active 
			if (can.readyToFire) {
				gameObject.GetComponent<Rigidbody> ().AddForce (shoot, ForceMode.Impulse); // add force to the player rigidbody + the speed we want 
				can_explosion.Play (); // play particle cannon shot 
				cannon_fire_sound.Play (); // play cannon fire 
				can.readyToFire = false; // not ready to fire anymore 
				act.activate.enabled = false; // disable text active 
				act.arrow.enabled = true; // enable arrow gui 
			}
		}
	}


	public void WestLiftMove ()
	{
		if (selectedAction == 1 || Input.GetButton ("Select")) {// if either b or push button and passage rotation is active 
			if (west_lift_up) {
				west_lift_moving = true; // lift is moving 
				if (canPlayel) { // can we play the elevator sound ?
					elev.Play (); // play the sound 
					canPlayel = false; // play sound only once 

				}
			}
		}

	}


	public void activateAnimation () // text animation 
	{
		sizeMax = 30; // max scaling size for active text 
		sizeMin = 20; // min scaling size for active text 
	

		if (act.activate.fontSize <= sizeMin) { // if text has not reached min scaling size

			active_scaler = false; // do not scale up anymore 
		} 

		if (act.activate.fontSize >= sizeMax) { // if text has not reached max scaling size
			active_scaler = true; // scale down 

		}


		if (active_scaler) { // switch boolean 
			act.activate.fontSize--; // reduce size 
		} 
		if (active_scaler == false) { // switch boolean 
			act.activate.fontSize++; // increase size 
		}
	}






}
