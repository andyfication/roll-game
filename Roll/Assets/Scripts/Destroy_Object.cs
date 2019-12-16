using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Object : MonoBehaviour
{

	public GameObject splash;
	// get spalsh animation
	public AudioSource splash_sound;
	// audio for the splsh animation 
	public float lifeTime;
	// lifetime of the object
	// Use this for initialization
	void Start ()
	{
		Destroy (gameObject, lifeTime); // destroy object after some time 
	}


	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Over")) { // if game_over and
		    // instanciate spalsh water animation at object position 
			GameObject temp = Instantiate (splash, new Vector3 (this.transform.position.x, this.transform.position.y - 0.3f, this.transform.position.z), Quaternion.identity) as GameObject;
			splash_sound.Play (); // play splash sound 
		}
	}
	

}
