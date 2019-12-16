using UnityEngine;
using System.Collections;

public class Mill_Rotation : MonoBehaviour
{

	public float speed;
	// speed for mill rotation

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0, 0, speed * Time.deltaTime); // rotate mill around the z axis 

	}
}
