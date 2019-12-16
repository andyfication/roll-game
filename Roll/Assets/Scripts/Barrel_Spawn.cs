using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel_Spawn : MonoBehaviour
{

	public Rigidbody barrel;
	// get barrel rigidbody reference
	public Transform start;
	// getting starting point of spawn


	

	void Start ()
	{
		InvokeRepeating ("FireBarrel", 20f, Random.Range (2f, 5f)); // repeat this function every 5f starting after 2f
	}

	void FireBarrel ()
	{
		Rigidbody barrelClone = (Rigidbody)(Instantiate (barrel.GetComponent<Rigidbody> (), start.position, start.rotation)); // spawn barrel object at start position 
	}
}
