using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class west_lift_moving : MonoBehaviour
{

	private Collisions cls;
	// getting Collision script reference
	public GameObject target;
	// target
	public GameObject start;
	// start position
	public float speed;
	// speed of motion
	// Use this for initialization
	void Start ()
	{
		cls = GameObject.Find ("Player").GetComponent<Collisions> (); // getting the Collision script attached to player 
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (cls.west_lift_moving) // if lift is moving or ready to move 
			transform.position = Vector3.MoveTowards (transform.position, target.transform.position, speed * Time.deltaTime); // transform to the target 
		else // if lift is moving back  or ready to move back  
			transform.position = Vector3.MoveTowards (transform.position, start.transform.position, speed * Time.deltaTime); // transform to start position 
	}
}
