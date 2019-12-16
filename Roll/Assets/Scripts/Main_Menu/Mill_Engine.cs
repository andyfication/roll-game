using UnityEngine;
using System.Collections;

public class Mill_Engine : MonoBehaviour {

	public float speed;  // mill rotation speed 

	// Update is called once per frame
	void Update () {
		transform.Rotate (speed * Time.deltaTime, 0, 0); // rotate the mill engine around the x axis
	}
}
