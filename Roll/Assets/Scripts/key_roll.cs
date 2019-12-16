using UnityEngine;
using System.Collections;

public class key_roll : MonoBehaviour
{

	public float speed = 0.1f;
	// speed for key rotation

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0, 0, speed * Time.deltaTime); // rotate the key object around the z axis 
	}
}
