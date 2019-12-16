using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Go_Green : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag("Finish")){ // if tutorial is finished 
			{
				SceneManager.LoadScene ("Transition_Grass"); // load Countryside image 
			}
		}
    }
}
