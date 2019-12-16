using UnityEngine;
using System.Collections;

public class Water_Mill_Rotation : MonoBehaviour {

	public float speed; // water mill rotation speed 

	// Update is called once per frame
	void Update () {
		transform.Rotate (-(speed * Time.deltaTime), 0, 0); // rotate around the x axis 
	}
}
