using UnityEngine;
using System.Collections;

public class Open_Door : MonoBehaviour
{

	private Collisions krl;
	// getting variables from another script
	public float speedOpen;
	// how fast does the door open

	// Use this for initialization
	void Start ()
	{
		krl = GameObject.Find ("Player").GetComponent<Collisions> (); // getting player collisions script 
		speedOpen = 0.2f; // speed of door opening = 0.2f
	}
	
	// Update is called once per frame
	void Update ()
	{
		openDoor (); // call function 
    }

	void openDoor ()
	{
		if (krl.key_collision) { // if the player  collects the key 
			Vector3 desiredAnge = new Vector3 (0, 270, 75); // desired angle for opeing door 
			if (Vector3.Distance (transform.eulerAngles, desiredAnge) > 0.01f) { // if the angle distance between the two angles is >0.01f
				transform.eulerAngles = Vector3.Lerp (transform.rotation.eulerAngles, desiredAnge, speedOpen * Time.deltaTime); // rotate and open door 
			} else {
				transform.eulerAngles = desiredAnge; // if is less set the door to desired angle 
				krl.key_collision = false; // no key is hold by the player anymore
			}
		}
	}
}
