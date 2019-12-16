using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Cannon : MonoBehaviour
{

	public bool inCannon;
	// is the player inside the cannon?
	public GameObject cannon;
	// object cannon
	public cannon_arrived can;
	// getting variables from another script
	// Use this for initialization
	public AudioSource cannonWheel;
	// audio for the cannon moving wheels 
	private bool playCannonWheel = true; // bool to keep track of the sound played 

	void Start ()
	{
		inCannon = false; // not in cannon at the beginning 
		can = GameObject.Find ("west_cannon").GetComponent<cannon_arrived> (); // getting the cannon_arrived script attached to west_cannon
	}
	
	// Update is called once per frame
	void Update ()
	{
	    if (inCannon && can.cannonNotArrived) // if player is inside the cannon and cannon position is not the end
			cannon.transform.Translate (-Time.deltaTime * 1f, 0f, 0f); // move cannon towarsd desired position 
	}


	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("cannon")) { // if collides with cannon 
			if(playCannonWheel) // can we play the sound ?
			{
			cannonWheel.Play (); // play the sound wheel 
			playCannonWheel = false; // play it ony once 
			inCannon = true; // player is inside the cannon 
			}
		}
	}
}
