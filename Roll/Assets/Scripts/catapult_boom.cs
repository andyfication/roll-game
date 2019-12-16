using UnityEngine;
using System.Collections;

public class catapult_boom : MonoBehaviour
{

	private bool boom;
	// boolean used to activate catapult
	public float boomRotation;
	// how much rotation should we apply
	private bool soundCat = true;
	// check catapult sound
	private Collisions cls;
	// getting a variable from another script
	public AudioSource caS;
	// audio used when the catapult is activated

	// Use this for initialization
	void Start ()
	{
		cls = GameObject.Find ("Player").GetComponent<Collisions> (); // getting the Collision script attached to player 
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (cls.selectedAction == 1 || Input.GetButton ("Select")) // if either b or the button is pushed and catapult is active 
			{
			if (cls.catOn) {
				if (soundCat) { // check if we can play the sound 
					caS.Play (); // play the sound 
					soundCat = false; // play the sound only once 
				}
				boom = true; // activate catapult 
				cls.catOn = false;
			}
		}
		CatBoom (); // call the activation function 
	}

	void CatBoom ()
	{
		if (boom) { // if active 
			Vector3 desiredAnge = new Vector3 (90, 90, 0); // how much we want to turn (target angle)
			if (Vector3.Distance (transform.eulerAngles, desiredAnge) > 0.01f) { // until the distance is grater than 0.01f between the two angles 
				transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, desiredAnge, boomRotation * Time.deltaTime); // perfomr rotation 
			} else {
				transform.eulerAngles = desiredAnge; // if distance is less we are at position 
				boom = false; // not active and stop rotating
			}
		}
	}
}
