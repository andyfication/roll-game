using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart_tutorial : MonoBehaviour {

	// Update is called once per frame
	void Update () {

		if (transform.position.y < -16) { // if in the tutorial the player falls 
			SceneManager.LoadScene ("Tutorial"); // reload the tutorial 
		}
		
	}
}
