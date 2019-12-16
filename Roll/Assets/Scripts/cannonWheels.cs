using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonWheels : MonoBehaviour
{
    public float speed;
	// speed of spinning cannon wheels
	public cannon_arrived can;
	// getting cannon arrived script reference
	// Use this for initialization
	void Start ()
	{
		can = GameObject.Find ("west_cannon").GetComponent<cannon_arrived> (); // getting the cannon_arrived script attached to west_cannon 
		speed = 100f; // speed of spinning for wheels 
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		if (can.cannonNotArrived == true) { // if the cannon is not arrived yet to position 
			gameObject.transform.Rotate (0f, 0f, Time.deltaTime * speed); // wheels animation on 
		}
	}
}
