using UnityEngine;
using System.Collections;
using Uniduino;
using UnityEngine.SceneManagement;

public class Camera_Player : MonoBehaviour
{

	public GameObject player;
	// player target
	private Vector3 offset;
	// Use this for initialization
	void Start ()
	{
		offset = transform.position - player.transform.position; // offset id the distance between the player and the camera
		Cursor.visible = false; // no mouse visible 
	
	}

	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.position = player.transform.position + offset; // camera position = player + offset already calculated 
	}
}
