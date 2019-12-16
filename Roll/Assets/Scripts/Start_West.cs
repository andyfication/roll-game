using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_West : MonoBehaviour {

	IEnumerator Start()
	{
		yield return new WaitForSeconds(10); // wait for ten seconds 
		SceneManager.LoadScene ("Dry_Western"); // load dry westenr
	}
}
