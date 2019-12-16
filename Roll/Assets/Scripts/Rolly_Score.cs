using UnityEngine;
using System.Collections;

public class Rolly_Score : MonoBehaviour
{

	public float speed = 0.1f;
	// speed coin rotation

	// Update is called once per frame
	void Update ()
	{
		transform.Rotate (0, speed * Time.deltaTime, 0); // rotate the coin around the y axis 
	}
}