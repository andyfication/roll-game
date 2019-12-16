using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fire_cannon : MonoBehaviour
{


	public float bulletSpeed = 10f;
	// speed of cannon bullets
	public Rigidbody bullet;
	// get bullet rigidbody
	public Transform start;
	// get starting position
	public AudioSource fire_bullet;
	// audio for cannon

	void Start ()
	{
		InvokeRepeating ("Fire", 5f, Random.Range (2f, 5f)); // repeat this function every 5f starting after 2f
	}

	void Fire ()
	{
		Rigidbody bulletClone = (Rigidbody)(Instantiate (bullet.GetComponent<Rigidbody> (), start.position, start.rotation)); // instanciate bullet at object position 
		bulletClone.velocity = transform.forward * bulletSpeed;// apply forward force to the bullet 
		fire_bullet.Play (); // play audio for cannon
	}


}
