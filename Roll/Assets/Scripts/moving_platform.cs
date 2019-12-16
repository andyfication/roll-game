using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform : MonoBehaviour
{

	public GameObject target;
	// target object
	public GameObject target1;
	// target object 1
	public float speed;
	// speed of motion
	private bool direction;
	// which direction is the platfrom moving

	// Use this for initialization
	void Start ()
	{
		speed = 0.9f; // setting speed 
		direction = true; // pick on direction 
	}
	
	// Update is called once per frame
	void Update ()
	{
		directionPlat (); // call function to choosing which direction to move 
		movePlat (); // function call to move platforms 
	}


	public void movePlat ()
	{
		if (direction) { // if this direction 
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, target1.transform.position, speed * Time.deltaTime); // transform position to target one  
		} else {
			gameObject.transform.position = Vector3.MoveTowards (gameObject.transform.position, target.transform.position, speed * Time.deltaTime); // transform position to target 
		}

	}

	public void directionPlat ()
	{
		if (gameObject.transform.position == target.transform.position) { // switch direction when platform reaches target
			direction = true; // switch direction 
		} else if (gameObject.transform.position == target1.transform.position) { // switch direction when platform reaches target1
			direction = false;// switch direction 
		}

	}
}
