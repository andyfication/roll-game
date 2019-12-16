using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannon_arrived : MonoBehaviour
{

	public bool cannonNotArrived;
	// boolean to keep track of the cannon position
	public bool readyToFire;
	// is the cannon ready to fire?
	public Over_Win act;
	// getting the Over_Win script reference
	// Use this for initialization
	public void Start ()
	{
		cannonNotArrived = true; // cannon not arrived yet 
		readyToFire = false; // cannon is not ready to fire
		act = GameObject.Find ("Menu").GetComponent<Over_Win> (); // getting the Over_win script attached to Menu 

	}


	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("cannon_end")) { // if colliding with the cannon end position 
		    cannonNotArrived = false; // cannon is arrived 
			readyToFire = true; // cannon is ready to fire 
			act.activate.enabled = true; // activate variable in Over_Win script 
			act.arrow.enabled = false; // disable bottom left gui arrow
		}
	}


}
