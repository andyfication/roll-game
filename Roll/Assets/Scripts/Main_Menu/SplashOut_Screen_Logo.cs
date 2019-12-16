using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class SplashOut_Screen_Logo : MonoBehaviour
{
	public Image splashOutLogo;
	// image with the game logo
	public string loadLevel;
	// sting to load next scene
	public AudioSource intro_bass;
	// playing some sound when the image fades out


	IEnumerator Start ()
	{
		Cursor.visible = false; // no cursor visible 
		splashOutLogo.canvasRenderer.SetAlpha (1.0f); // logo is visible 
		yield return new WaitForSeconds (2.5f); // wait for 2.5f 
		FadeOutLogo (); // call function 
		yield return new WaitForSeconds (2.5f); // wait for two 2.5f
		SceneManager.LoadScene (loadLevel); // load the next scene 

	}

	void FadeOutLogo ()
	{

		splashOutLogo.CrossFadeAlpha (0f, 1.5f, false); // fade out logo 
		intro_bass.Play (); // play the sound on fade out 
	
	}

}
