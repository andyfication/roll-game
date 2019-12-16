using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine_Cart_Motion : MonoBehaviour
{

	public GameObject[] waypoints;
	// set of waypoint objects
	private int currentIndex;
	// current object in the list
	public float speed;
	// speed value
	public bool cart_arrived;
	// is the minecart arrived?
	public ParticleSystem explosion;
	// partycle system object
	private Collisions krl;
	// getting variables from another script

	// Use this for initialization
	void Start ()
	{
		krl = GameObject.Find ("Player").GetComponent<Collisions> (); // getting player collisions script 
		speed = 2f; // setting speed to 2f
		currentIndex = 0; // starting from first index 
		cart_arrived = false; // mine cart not arrived 
		explosion.GetComponent<Particle> (); // get particle component from particlesystem
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (krl.mineCartMoving) // if the cart can move  
		Follow_Path (); // activate mine cart call function 
		Debug.Log (cart_arrived); // little check during debugging 
	}



	void Follow_Path ()
	{
		float distance = Vector3.Distance (gameObject.transform.position, waypoints [currentIndex].transform.position); // get the distance between start point and the first target 

		if (distance > 0.5f) { // if distance is more than 0.5f 
			Vector3 targetDir = waypoints [currentIndex].transform.position - transform.position; // get direction 
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, speed, 0.0F); // rotate towards direction 
			Debug.DrawRay (transform.position, newDir, Color.red); // debug line
			transform.rotation = Quaternion.LookRotation (newDir); // look at newdirection 
			gameObject.transform.position += gameObject.transform.forward * speed * Time.deltaTime; // transform the minecart position towards target 
		} else { // if resched first target 
			if (currentIndex < waypoints.Length - 1) // not out of array
				currentIndex++; // increase the target index 
		}
	}



	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("cartEnd")) { // if minecart collides with end position 
			krl.cart_mine_sound.Stop();
			cart_arrived = true; // the minecart is arrived 
			explosion.Play (); // play the explosion animation 
			if (cart_arrived) { // if cart is at the end 
				krl.cart_collision_sound.Play (); // play collision rock sound 
			}
		}
	}
}
