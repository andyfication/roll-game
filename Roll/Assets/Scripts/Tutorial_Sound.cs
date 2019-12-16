using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Sound : MonoBehaviour
{

	public AudioSource one;
	// audio for the tutorial (beginning)
	public AudioSource two;
	// audio for the tutorial (collect score)
	public AudioSource three;
	// audio for the tutorial (platforms)
	public AudioSource four;
	// audio for the tutorial (particles )
	public AudioSource five;
	// audio for the tutorial (roll safe)

	public AudioClip one_s;
	// audioclip for the sound
	public AudioClip two_s;
	// audioclip for the sound
	public AudioClip three_s;
// audioclip for the sound
	public AudioClip four_s;
// audioclip for the sound
	public AudioClip five_s;
// audioclip for the sound


	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("tut_beg")) { // when we collide with such a tag 
			one.PlayOneShot (one_s, 0.7f);// play sound (roll beginning)
			two.Stop(); // second sound not active 
		}
		if (other.gameObject.CompareTag ("two")) { // when we collide with such a tag 
			two.PlayOneShot (two_s, 0.7f); // play sound  (collect score)
			// stop other sounds 
			one.Stop();
			three.Stop();
			four.Stop ();
			five.Stop ();
			}

		if (other.gameObject.CompareTag ("three")) { // when we collide with such a tag 
			three.PlayOneShot (three_s, 0.7f);// play sound  (platforms)
			// stop other sounds 
			one.Stop();
			two.Stop();
			four.Stop ();
			five.Stop ();

		}

		if (other.gameObject.CompareTag ("four")) { // when we collide with such a tag 
			four.PlayOneShot (four_s, 0.7f);// play sound (particles )
			// stop other sounds 
			one.Stop();
			two.Stop();
			three.Stop ();
			five.Stop ();
		}
		if (other.gameObject.CompareTag ("five")) { // when we collide with such a tag 
			five.PlayOneShot (five_s, 0.7f);// play sound (roll safe)
			// stop other sounds 
			one.Stop();
			two.Stop();
			three.Stop ();
			four.Stop ();
		}


	}
}
