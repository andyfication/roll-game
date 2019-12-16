using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Start_Grass : MonoBehaviour {

	IEnumerator Start()
	{
		yield return new WaitForSeconds(10); // wait for ten seconds 
		SceneManager.LoadScene ("Green_Countryside"); // load green countryside 
	}
}
