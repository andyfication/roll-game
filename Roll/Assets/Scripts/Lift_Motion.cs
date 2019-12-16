using UnityEngine;
using System.Collections;


public class Lift_Motion : MonoBehaviour
{

	public float speed;
	// speed fot the lift motion
	public float speedRotation;
	// speed for lift rotation
	public Transform destination;
	// target
	public Transform start;
	// starting point
	private bool rotating;
	// is rotating ?
	private Collisions cls;
	// getting variables from another script



	// Use this for initialization
	void Start ()
	{
		speed = 2; // speed to 2 
		cls = GameObject.Find ("Player").GetComponent<Collisions> (); // getting collision script from player 
	}


	
	// Update is called once per frame
	void FixedUpdate ()
	{
		if (cls.lift_Collision) { // if lift is ready to go 
			rotating = true; // rotate = true 
			rotateWhenArrive (); // call function 
			transform.position = Vector3.MoveTowards (transform.position, destination.position, speed * Time.deltaTime); // trnaform to target ( up)

		} else if (!cls.lift_Collision) { // if lift is not active 
			rotating = true; // rotate = true 
			rotateWhenLeave (); // call function 
			transform.position = Vector3.MoveTowards (transform.position, start.position, speed * Time.deltaTime); // transform to target (down)
		}
	}



	void rotateWhenArrive () // moving up 
	{
		if (rotating) { // if is rotating 
			Vector3 desiredAnge = new Vector3 (0, 180, 0); // set the desiredAngle 
			if (Vector3.Distance (transform.eulerAngles, desiredAnge) > 0.01f) { // if th distance between the tw angles id more than 0.01f
				transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, desiredAnge, speedRotation * Time.deltaTime); // perform rotation 
			} else {
				transform.eulerAngles = desiredAnge; // if it is less we move to desired angle 
				rotating = false; // not rotating anymore 
			}
		}
	}

	void rotateWhenLeave () // moving down 
	{
		if (rotating) { // if rotating 
			Vector3 desiredAnge1 = new Vector3 (0, 90, 0);// set the desiredAngle 
			if (Vector3.Distance (transform.eulerAngles, desiredAnge1) > 0.01f) {// if th distance between the tw angles id more than 0.01f
				transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, desiredAnge1, speedRotation * Time.deltaTime); // perform rotation 
			} else {
				transform.eulerAngles = desiredAnge1; // if it is less we move to desired angle 
				rotating = false;// not rotating anymore 
			}
		}
	}

}


