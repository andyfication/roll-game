using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lock_mouse : MonoBehaviour {

	// Use this for initialization
	void Start () {

		Screen.lockCursor = true;
		
	}


	void Update()
	{


		if (Input.GetKey (KeyCode.Escape)) {
			Application.Quit ();
		}

	}
	

}
