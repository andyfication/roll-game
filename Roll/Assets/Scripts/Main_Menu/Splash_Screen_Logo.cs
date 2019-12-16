using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Splash_Screen_Logo : MonoBehaviour
{
	
	public Text presents;
	// 'presents: text '
	public string loadLevel;
	// string of the level to load
	float width;
	// width value
	float height;
	// height value
	bool switchScale;
	// used to reverse the scaling


	IEnumerator Start ()
	{
		Cursor.visible = false; // not cursor is visible 
		switchScale = false; // sclaing in one direction 
		presents.canvasRenderer.SetAlpha (0.0f); // text not visible 
		yield return new WaitForSeconds (1.5f); // wait for 1.5f
		presents.canvasRenderer.SetAlpha (1.0f); // text visible 
		FadeOut (); // call function 
		yield return new WaitForSeconds (3.5f); // wait for 3.5f
		SceneManager.LoadScene (loadLevel); // load next scene 

	}

	void FadeOut ()
	{
		switchScale = false; // stop scaling 
		presents.CrossFadeAlpha (0.0f, 3f, false); // fade out text 

	}
}
